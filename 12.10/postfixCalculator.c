#include "../Modules/customStack.h"
#include <stdio.h>

#define maxLineSize 100

bool isDigit(char element) {
    if (element == '0' || element == '1' || element == '2' || element == '3' || element == '4' || element == '5'
    || element == '6' || element == '7' || element == '8' || element == '9') {
        return true;
    }
    return false;
}

void evaluateStack(Stack *stack, int *returnErrorCode, Stack *operations) {
    if (isEmpty(stack)) {
        return;
    }
    char *topElement = pop(stack, returnErrorCode);
    if (*returnErrorCode) {
        return;
    }
    int topElementSize = 0;
    while (topElement[topElementSize + 1] != '\0') {
        ++topElementSize;
    }

    if (isDigit(topElement[topElementSize])) {
        evaluateStack(stack, returnErrorCode, operations);
        if (*returnErrorCode) {
            return;
        }
        push(stack, topElement);
    } else {
        topElementSize += 1;
        char operation = topElement[topElementSize - 1];
        push(operations, &operation);
        int numOfDigits = 1;
        char firstArgument[maxLineSize] = {0};
        int firstArgumentEndIndex = 0;
        char secondArgument[maxLineSize] = {0};
        int secondArgumentStartIndex = 0;

        for (int i = topElementSize - 3; i >= 0; i--) {
            if (topElement[i] == ' ') {
                continue;
            }
            if (isDigit(topElement[i])) {
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
            firstArgument[i] = topElement[i];
        }
        int secondArgumentIndex = 0;
        for (int i = secondArgumentStartIndex; i < topElementSize - 2; i++) {
            secondArgument[secondArgumentIndex] = topElement[i];
            ++secondArgumentIndex;
        }

        push(stack, firstArgument);
        push(stack, secondArgument);
        evaluateStack(stack, returnErrorCode, operations);
        if (*returnErrorCode) {
            return;
        }
    }
}

int main() {
    Stack *stack = createStack();
    Stack *operations = createStack();
    push(stack, "9 6 - 1 2 + *");
    int errorCode = 0;
    evaluateStack(stack, &errorCode, operations);
    if (errorCode) {
        printf("Возникла ошибка в функции подсчёта стэка");
        return -1;
    }
    return 0;
}
