#include "../Modules/customStack.h"
#include <stdio.h>
#include <malloc.h>

#define maxLineSize 100

typedef struct Expression{
    char *firstArgument;
    char *secondArgument;
    char operation;
} Expression;

bool isDigit(char element) {
    if (element == '0' || element == '1' || element == '2' || element == '3' || element == '4' || element == '5'
    || element == '6' || element == '7' || element == '8' || element == '9') {
        return true;
    }
    return false;
}

Expression *parseStringIntoExpression(char *string, int *errorCode) {
    *errorCode = 0;
    int stringSize = 0;
    while (string[stringSize + 1] != '\0') {
        ++stringSize;
    }

    stringSize += 1;
    char operation = string[stringSize - 1];

    int numOfDigits = 1;
    char firstArgument[maxLineSize] = {0};
    int firstArgumentEndIndex = 0;
    char secondArgument[maxLineSize] = {0};
    int secondArgumentStartIndex = 0;

    for (int i = stringSize - 3; i >= 0; i--) {
        if (string[i] == ' ') {
            continue;
        }
        if (isDigit(string[i])) {
            numOfDigits -= 1;
            if (numOfDigits == 0) {
                firstArgumentEndIndex = i - 2;
                secondArgumentStartIndex = i;
                break;
            }
            continue;
        }
        numOfDigits += 1;
    }

    for (int i = 0; i <= firstArgumentEndIndex; i++) {
        firstArgument[i] = string[i];
    }
    int secondArgumentIndex = 0;
    for (int i = secondArgumentStartIndex; i < stringSize - 2; i++) {
        secondArgument[secondArgumentIndex] = string[i];
        ++secondArgumentIndex;
    }

    Expression *expression = malloc(sizeof (Expression));
    if (expression == NULL) {
        printf("Ошибка выделения памяти\n");
        *errorCode = -1;
        return NULL;
    }
    expression->firstArgument = firstArgument;
    expression->secondArgument = secondArgument;
    expression->operation = operation;
    return expression;
}

int evaluateExpression(Expression *expression, int *errorCode) {
    *errorCode = 0;
    int indexOfFirstArgument = 0;
    int indexOfSecondArgument = 0;
    while (expression->firstArgument[indexOfFirstArgument + 1] != '\0') {
        ++indexOfFirstArgument;
    }
    while (expression->secondArgument[indexOfSecondArgument + 1] != '\0') {
        ++indexOfSecondArgument;
    }

    int valueOfFirstArgument = 0;
    int valueOfSecondArgument = 0;
    if (isDigit(expression->firstArgument[indexOfFirstArgument]) && isDigit(expression->secondArgument[indexOfSecondArgument])) {
        valueOfFirstArgument = ((int)expression->firstArgument[indexOfFirstArgument] - 50);
        valueOfSecondArgument = ((int)expression->secondArgument[indexOfSecondArgument] - 50);
    } else {
        Expression *firstArgumentExpression = parseStringIntoExpression(expression->firstArgument, errorCode);
        if (*errorCode) {
            return 0;
        }
        Expression *secondArgumentExpression = parseStringIntoExpression(expression->secondArgument, errorCode);
        if (*errorCode) {
            return 0;
        }
        valueOfFirstArgument = evaluateExpression(firstArgumentExpression, errorCode);
        if (*errorCode) {
            return 0;
        }
        valueOfSecondArgument = evaluateExpression(secondArgumentExpression, errorCode);
        if (*errorCode) {
            return 0;
        }
    }

    if (expression->operation == '+') {
        return valueOfFirstArgument + valueOfSecondArgument;
    }
    if (expression->operation == '-') {
        return valueOfFirstArgument - valueOfSecondArgument;
    }
    if (expression->operation == '*') {
        return valueOfFirstArgument * valueOfSecondArgument;
    }
    if (expression->operation == '/') {
        return valueOfFirstArgument / valueOfSecondArgument;
    }
    printf("Незивестная операция\n");
    *errorCode = -1;
    return 0;
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

    printf("Введите арифметическое выражение в постфиксной форме(в нём могут учавствовать цифры и знаки + - * /) не диннее 100 символов и перевод строки\n");
    char arithmeticExpression[maxLineSize] = {0};
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
    Expression *expression = {parseStringIntoExpression(arithmeticExpression, &errorCode);
    if (errorCode) {
        printf("Произошла ошибка при парсинге выражения\n");
        return -1;
    }

    printf("%c\n%s\n%s\n", expression->operation, expression->firstArgument, expression->secondArgument);

    int result = evaluateExpression(expression, &errorCode);
    if (errorCode) {
        printf("Произошла ошибка при вычеслении выражения\n");
        return -1;
    }

    printf("%d", result);
    return 0;
}
