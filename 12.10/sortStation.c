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

bool isOperator(char element) {
    if (element == '+' || element == '-' || element == '*' || element == '/') {
        return true;
    }
    return false;
}

bool isPriorityOfSecondOperatorHigherOrEqual(char firstOperator, char secondOperator) {
    if (secondOperator == '*' || secondOperator == '/') {
        return true;
    }
    if (firstOperator == '+' || firstOperator == '-') {
        return true;
    }
    return false;
}

char *parseInfixStringIntoPostfixString(const char *string, int *errorCode) {
    Stack *stack = createStack();
    char *outputExpressionInPrefixForm = calloc(maxLineSize + 1, sizeof(char));
    int indexInOutputExpression = 0;
    for (int i = 0; i < maxLineSize; i++) {
        if (string[i] == '\0') {
            break;
        }
        if (string[i] == ' ') {
            continue;
        }

        if (isDigit(string[i])) {
            if (indexInOutputExpression + 1 >= maxLineSize) {
                printf("При преобразовании максимальное количество элементов в выражении превышено\n");
                *errorCode = 1;
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
                    printf("Попаю пустой стэк, вероятно не правильная арифметическая запись\n");
                    return NULL;
                }
                if (indexInOutputExpression + 1 >= maxLineSize) {
                    printf("При преобразовании максимальное количество элементов в выражении превышено\n");
                    *errorCode = 1;
                    return  NULL;
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
                    printf("Попаю пустой стэк, вероятно не правильная арифметическая запись(проблема со скобками)\n");
                    return NULL;
                }
                if (indexInOutputExpression + 1 >= maxLineSize) {
                    printf("При преобразовании максимальное количество элементов в выражении превышено\n");
                    *errorCode = 1;
                    return  NULL;
                }
                outputExpressionInPrefixForm[indexInOutputExpression] = topValue;
                outputExpressionInPrefixForm[++indexInOutputExpression] = ' ';
                ++indexInOutputExpression;
            }
            pop(stack, errorCode);
            if (*errorCode) {
                printf("Попаю пустой стэк, вероятно не правильная арифметическая запись\n");
                return NULL;
            }
        }
        *errorCode = 1;
        printf("Неизвестный символ в выражении\n");
        return NULL;
    }
    while (!isEmpty(stack)) {
        char topValue = pop(stack, errorCode);
        if (*errorCode) {
            printf("Попаю пустой стэк, вероятно не правильная арифметическая запись\n");
            return NULL;
        }
        if (topValue == '(') {
            *errorCode = 1;
            printf("Попаю пустой стэк, вероятно не правильная арифметическая запись(проблема со скобками)\n");
            return NULL;
        }
        if (indexInOutputExpression + 1 >= maxLineSize) {
            printf("При преобразовании максимальное количество элементов в выражении превышено\n");
            *errorCode = 1;
            return  NULL;
        }
        outputExpressionInPrefixForm[indexInOutputExpression] = topValue;
        outputExpressionInPrefixForm[++indexInOutputExpression] = ' ';
        ++indexInOutputExpression;
    }
    return outputExpressionInPrefixForm;
}