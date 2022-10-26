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
    }
    return outputExpressionInPrefixForm;
}