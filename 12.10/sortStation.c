#include "../Modules/customStack.h"
#include <stdio.h>
#include <malloc.h>
#include <string.h>

#define MAX_LINE_SIZE 100

bool isDigit(char element) {
    return element >= '0' && element <= '9';
}

bool isOperator(char element) {
    return element == '+' || element == '-' || element == '*' || element == '/';
}

bool isPriorityOfSecondOperatorHigherOrEqual(char firstOperator, char secondOperator) {
    return secondOperator == '*' || secondOperator == '/' || firstOperator == '+' || firstOperator == '-';
}

char *parseInfixStringIntoPostfixString(const char *string, int *errorCode) {
    Stack *stack = createStack();
    char *outputExpressionInPrefixForm = calloc(MAX_LINE_SIZE + 1, sizeof(char));
    int indexInOutputExpression = 0;
    for (int i = 0; i < MAX_LINE_SIZE; i++) {
        if (string[i] == '\0') {
            break;
        }
        if (string[i] == ' ') {
            continue;
        }

        if (isDigit(string[i])) {
            if (indexInOutputExpression + 1 >= MAX_LINE_SIZE) {
                *errorCode = 1;
                deleteStack(stack);
                free(outputExpressionInPrefixForm);
                return NULL;
            }
            outputExpressionInPrefixForm[indexInOutputExpression] = string[i];
            outputExpressionInPrefixForm[++indexInOutputExpression] = ' ';
            ++indexInOutputExpression;
            continue;
        }
        if (isOperator(string[i])) {
            while (!isEmpty(stack) && isOperator(top(stack)) && isPriorityOfSecondOperatorHigherOrEqual(string[i], top(stack))) {
                char secondOperator = pop(stack, errorCode);
                if (*errorCode) {
                    deleteStack(stack);
                    free(outputExpressionInPrefixForm);
                    return NULL;
                }
                if (indexInOutputExpression + 1 >= MAX_LINE_SIZE) {
                    *errorCode = 1;
                    deleteStack(stack);
                    free(outputExpressionInPrefixForm);
                    return NULL;
                }
                outputExpressionInPrefixForm[indexInOutputExpression] = secondOperator;
                outputExpressionInPrefixForm[++indexInOutputExpression] = ' ';
                ++indexInOutputExpression;
            }
            push(stack, string[i]);
            continue;
        }
        if (string[i] == '(') {
            push(stack, string[i]);
            continue;
        }
        if (string[i] == ')') {
            //Добавить в топ проверку на пустоту стэка
            while (top(stack) != '(') {
                char topValue = pop(stack, errorCode);
                if (*errorCode) {
                    deleteStack(stack);
                    free(outputExpressionInPrefixForm);
                    return NULL;
                }
                if (indexInOutputExpression + 1 >= MAX_LINE_SIZE) {
                    *errorCode = 1;
                    deleteStack(stack);
                    free(outputExpressionInPrefixForm);
                    return  NULL;
                }
                outputExpressionInPrefixForm[indexInOutputExpression] = topValue;
                outputExpressionInPrefixForm[++indexInOutputExpression] = ' ';
                ++indexInOutputExpression;
            }
            pop(stack, errorCode);
            if (*errorCode) {
                deleteStack(stack);
                free(outputExpressionInPrefixForm);
                return NULL;
            }
            continue;
        }
        *errorCode = 1;
        deleteStack(stack);
        free(outputExpressionInPrefixForm);
        return NULL;
    }
    while (!isEmpty(stack)) {
        char topValue = pop(stack, errorCode);
        if (*errorCode) {
            deleteStack(stack);
            free(outputExpressionInPrefixForm);
            return NULL;
        }
        if (topValue == '(') {
            *errorCode = 1;
            deleteStack(stack);
            free(outputExpressionInPrefixForm);
            return NULL;
        }
        if (indexInOutputExpression + 1 >= MAX_LINE_SIZE) {
            *errorCode = 1;
            deleteStack(stack);
            free(outputExpressionInPrefixForm);
            return NULL;
        }
        outputExpressionInPrefixForm[indexInOutputExpression] = topValue;
        outputExpressionInPrefixForm[++indexInOutputExpression] = ' ';
        ++indexInOutputExpression;
    }
    deleteStack(stack);
    return outputExpressionInPrefixForm;
}

bool test() {
    bool testResult = true;
    char infixExpression[] = "1+3*2/3";
    int errorCode = 0;
    char *postfixExpression = parseInfixStringIntoPostfixString(infixExpression, &errorCode);
    if (!strcmp(postfixExpression, "1 3 2 * 3 / +")) {
        testResult = false;
    }
    free(postfixExpression);
    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены\n");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите выражение в инфиксной форме, состоящее из цифр и знаков +,"
           " -, *, / и содержащее не более %d символов\n", MAX_LINE_SIZE);
    char infixExpression[MAX_LINE_SIZE + 1] = {0};
    scanf("%s", infixExpression);

    int errorCode = 0;
    char *postfixExpression = parseInfixStringIntoPostfixString(infixExpression, &errorCode);
    printf("Введённое выражение, переведённое в постфиксную форму: %s\n", postfixExpression);
    if (errorCode) {
        printf("Ошибка при парсинге выражения\n");
        return -1;
    }

    free(postfixExpression);
    return 0;
}