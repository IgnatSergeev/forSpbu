#include "../Modules/customStack.h"
#include <stdio.h>
#include <malloc.h>

#define maxLineSize 100

bool isBracketSequenceCorrect(char bracketSequence[], int *errorCode) {
    Stack *stack = createStack();
    bool bracketSequenceCorrect = true;
    for (int i = 0; i < maxLineSize; i++) {
        if (bracketSequence[i] == '\0') {
            break;
        }

        char currentBracket = bracketSequence[i];
        switch (currentBracket) {
            case '(': {

            }
        }
    }
}

bool test() {
    bool testResult = true;

    int errorCode = 0;
    if (evaluateExpression("9 6 - 1 2 + *", 13, &errorCode) != 9) {
        testResult = false;
    }
    if (errorCode) {
        printf("Возникла ошибка в вычислении результата");
        return false;
    }
    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты не пройдены\n");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите скобочную запись(в ней могут учавствовать 3 вида скобок {([])}) не диннее %d символов и перевод строки\n", maxLineSize - 1);
    char inputBracketSequences[maxLineSize] = {0};
    scanf("%s", inputBracketSequences);

    int errorCode = 0;
    bool isInputBracketSequenceCorrect = isBracketSequenceCorrect(inputBracketSequences, &errorCode);
    if (errorCode) {
        printf("Возникла ошибка в вычислении результата\n");
        return -1;
    }
    if (isInputBracketSequenceCorrect) {
        printf("Запись корректна\n");
    } else {
        printf("Запись некорректна\n");
    }
    return 0;
}