#include <stdio.h>
#include <string.h>
#include "../Modules/BinaryTree/customBinaryTree.h"

#define MAX_LINE_SIZE 100

Type whatIfEqualWhenAdding(Type oldValue, Type newValue) {
    oldValue.value = newValue.value;
    return oldValue;
}

Type whatIfEqualWhenSearching(Type oldValue, Type newValue) {
    return oldValue;
}

int compare(Type oldValue, Type newValue) {
    return (oldValue.key == newValue.key) ? 0 : (oldValue.key < newValue.key) ? -1 : 1;
}

bool test(void) {
    bool testResult = true;
    BinaryTree *testBinaryTree = create();
    Type zeroValue = {0};
    int errorCode = 0;

    Type value1 = {1 , "asd"};
    addValue(testBinaryTree, value1, &compare, &whatIfEqualWhenAdding);

    Type value2 = {2 , "asx"};
    addValue(testBinaryTree, value2, &compare, &whatIfEqualWhenAdding);

    testResult = !strcmp("asx", findValue(testBinaryTree, value2, &errorCode, zeroValue, &compare, &whatIfEqualWhenSearching).value);
    if (errorCode) {
        return false;
    }

    if (deleteValue(testBinaryTree, value1, compare)) {
        return false;
    }

    testResult = testResult && (findValue(testBinaryTree, value1, &errorCode, zeroValue, &compare, &whatIfEqualWhenSearching).value == NULL);
    if (errorCode) {
        return false;
    }

    testResult = testResult && !strcmp("asx", findValue(testBinaryTree, value2, &errorCode, zeroValue, &compare, &whatIfEqualWhenSearching).value);
    if (errorCode) {
        return false;
    }

    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    BinaryTree *binaryTree = create();
    Type zeroValue = {0, NULL};

    while (true) {
        int userInput = 0;
        printf("0 - Выйти из программы.\n"
                "1 - Добавить значение по заданному ключу в словарь. Если такой ключ уже есть, значение заменяется на новое.\n"
               "2 - Получить значение по заданному ключу из словаря. Если такого ключа нет, возвращается NULL.\n"
               "3 - Проверить наличие заданного ключа в словаре.\n"
               "4 - Удалить заданный ключ и связанное с ним значение из словаря. Если такого ключа нет, функция ничего не делает.\n");
        scanf("%d", &userInput);
        bool endCondition = false;
        switch (userInput) {
            case 0: {
                endCondition = true;
                break;
            } case 1: {
                printf("Введите ключ и значение элемента, который хотите добавить в словарь\n");
                char inputValue[MAX_LINE_SIZE] = {0};
                int inputKey = 0;
                scanf("%d", &inputKey);
                scanf("%s", inputValue);
                Type element = {inputKey, inputValue};
                int errorCode = addValue(binaryTree, element, &compare, &whatIfEqualWhenAdding);
                if (errorCode) {
                    printf("Возникла ошибка\n");
                }
                break;
            } case 2: {
                printf("Введите ключ элемента, значение которого хотите найти в словаре\n");
                int inputKey = 0;
                scanf("%d", &inputKey);
                Type element = {inputKey, NULL};
                int errorCode = 0;
                Type returnValue = findValue(binaryTree, element, &errorCode, zeroValue, &compare, &whatIfEqualWhenSearching);
                if (errorCode) {
                    printf("Произошла ошибка\n");
                    break;
                }
                printf("Значение элемента по ключу = %s\n", returnValue.value);
                break;
            } case 3: {
                printf("Введите ключ, наличие которого в словаре вы хотите проверить\n");
                int inputKey = 0;
                scanf("%d", &inputKey);
                Type element = {inputKey, NULL};
                int errorCode = 0;
                bool returnValue = isTheValueInTree(binaryTree, element, &errorCode, &compare);
                if (errorCode) {
                    printf("Произошла ошибка\n");
                    break;
                }
                if (returnValue) {
                    printf("Такой ключ присутствует\n");
                } else {
                    printf("Такого ключа нет\n");
                }
                break;
            } case 4: {
                printf("Введите ключ элемента, который хотите удалить из словаря\n");
                int inputKey = 0;
                scanf("%d", &inputKey);
                Type element = {inputKey, NULL};
                int errorCode = deleteValue(binaryTree, element, &compare);
                if (errorCode) {
                    printf("Произошла ошибка\n");
                }
                break;
            } default: {
                printf("Неизвестный ввод - повторите попытку\n");
            }
        }

        if (endCondition) {
            break;
        }
    }

    clear(binaryTree);
    return 0;
}
