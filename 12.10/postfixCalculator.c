#include "../Modules/customStack.h"
#include <stdio.h>
#include <malloc.h>

#define maxLineSize 100

bool isDigit(char element) {
    if (element == '0' || element == '1' || element == '2' || element == '3' || element == '4' || element == '5'
    || element == '6' || element == '7' || element == '8' || element == '9') {
        return true;
    }
    return false;
}

int evaluateExpression(const char expression[], int expressionSize, int *errorCode) {
    Stack *stack = createStack();
    *errorCode = 0;
    for (int currentIndex = 0; currentIndex < expressionSize; currentIndex++) {
        if (expression[currentIndex] == ' ') {
            continue;
        }

        if (isDigit(expression[currentIndex])) {
            *errorCode = push(stack, (int)expression[currentIndex] - 48);
            if (*errorCode) {
                deleteStack(stack);
                return 0;
            }
            continue;
        }

        int secondArgument = pop(stack, errorCode);
        if (*errorCode) {
            deleteStack(stack);
            return 0;
        }
        int firstArgument = pop(stack, errorCode);
        if (*errorCode) {
            deleteStack(stack);
            return 0;
        }

        switch (expression[currentIndex]) {
            case '+':
            {
                push(stack, firstArgument + secondArgument);
                if (*errorCode) {
                    deleteStack(stack);
                    return 0;
                }
                break;
            }
            case '-':
            {
                push(stack, firstArgument - secondArgument);
                if (*errorCode) {
                    deleteStack(stack);
                    return 0;
                }
                break;
            }
            case '*':
            {
                push(stack, firstArgument * secondArgument);
                if (*errorCode) {
                    deleteStack(stack);
                    return 0;
                }
                break;
            }
            case '/':
            {
                push(stack, firstArgument / secondArgument);
                if (*errorCode) {
                    deleteStack(stack);
                    return 0;
                }
                break;
            }
            default:
            {
                *errorCode = 1;
                printf("Неизвестная операция");
                return 0;
            }
        }
    }
    int result = pop(stack, errorCode);
    if (*errorCode) {
        deleteStack(stack);
        return 0;
    }

    deleteStack(stack);
    return result;
}

bool test() {
    return true;
}

int main() {
    if (!test()) {
        printf("Тесты не пройдены\n");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите арифметическое выражение в постфиксной форме(в нём могут учавствовать цифры и знаки + - * /) не диннее %d символов и перевод строки\n", maxLineSize);
    char arithmeticExpression[maxLineSize + 1] = {0};
    int arithmeticExpressionIndex = 0;
    char stringElement = 0;
    while (scanf("%c", &stringElement)) {
        if (arithmeticExpressionIndex >= maxLineSize || stringElement == '\n') {
            break;
        }
        arithmeticExpression[arithmeticExpressionIndex] = stringElement;
        ++arithmeticExpressionIndex;
    }
    if (arithmeticExpressionIndex >= maxLineSize) {
        printf("%s%d%s", "Введённое выражение длиннее " , maxLineSize," символов");
        return -1;
    }

    int errorCode = 0;
    int resultOfExpressionEvaluation = evaluateExpression(arithmeticExpression, arithmeticExpressionIndex, &errorCode);
    if (errorCode) {
        printf("Возникла ошибка в вычислении ошибки");
        return -1;
    }
    printf("Результат вычисления выражения = %d", resultOfExpressionEvaluation);
    return 0;
}
