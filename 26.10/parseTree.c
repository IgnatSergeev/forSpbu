#include "../Modules/BinaryTree/customBinaryTree.h"
#include <stdio.h>

#define MAX_LINE_SIZE 100

Type whatIfEqualInAdding(Type value1, Type value2, int *errorCode) {
    *errorCode = 1;
    return value1;
}

Type whatToDoInTheEndOfRight(Type currentNodeValue, Type leftSonValue, Type rightSonValue) {
    currentNodeValue.isSubtreeFull = leftSonValue.isSubtreeFull && rightSonValue.isSubtreeFull;
    return currentNodeValue;
}

bool isDigit(char symbol) {
    return symbol >= '0' && symbol <= '9';
}

int readTheExpressionIntoTreeFromFile(FILE *file, BinaryTree *parseTree) {
    char inputChar = (char)fgetc(file);
    while (inputChar != -1) {
        if (inputChar == ' ' || inputChar == '(' || inputChar == ')') {
            inputChar = (char)fgetc(file);
            continue;
        }

        if (!isDigit(inputChar)) {
            Type value = {operation, inputChar, 0, false};
            if (addValue(parseTree, value, NULL, &whatIfEqualInAdding, &whatToDoInTheEndOfRight)) {
                return -1;
            }
        } else {
            ungetc(inputChar, file);
            int currentNumber = 0;
            fscanf(file, "%d", &currentNumber);
            Type value = {number, '\0', currentNumber, true};
            if (addValue(parseTree, value, NULL, &whatIfEqualInAdding, &whatToDoInTheEndOfRight)) {
                return -1;
            }
        }

        inputChar = (char)fgetc(file);
    }
    return 0;
}

bool test(void) {
    bool testResult = true;
    FILE *file = fopen("../26.10/testParseTree.txt", "r");
    if (file == NULL) {
        return false;
    }

    BinaryTree *parseTree = create();
    if (parseTree == NULL) {
        return false;
    }

    readTheExpressionIntoTreeFromFile(file, parseTree);

    int evaluatedValue = 0;
    treeTraversal(parseTree, NULL, &evaluatedValue);

    testResult = evaluatedValue == 4;

    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    BinaryTree *parseTree = create();
    if (parseTree == NULL) {
        printf("Произошла ошибка при создании дерева разбора\n");
        return -1;
    }

    printf("Программа прочитает выражение из файла parseTree.txt и вычислит его (в нём могут быть знаки +,-,*,/ и неотрицательные целые числа)\n");

    FILE *file = fopen("../26.10/parseTree.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");
        clear(parseTree);
        return -1;
    }

    if (readTheExpressionIntoTreeFromFile(file, parseTree)) {
        printf("Ошибка - неправильная скобочная запись\n");
        clear(parseTree);
        return -1;
    }

    printf("Вот прочитанное выражение: ");
    printTree(parseTree);
    printf("\n");

    int evaluatedValue = 0;
    treeTraversal(parseTree, NULL, &evaluatedValue);

    printf("Вычисленное значение выражения = %d\n", evaluatedValue);
    clear(parseTree);
    return 0;
}