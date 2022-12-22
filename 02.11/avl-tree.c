#include "../Modules/AVL Tree/avlTree.h"
#include "string.h"

int compare(Type firstValue, Type secondValue) {
    return strcmp(firstValue.key, secondValue.key);
}

void whatToDoIfEqualInAdding(Type oldValue, Type newValue) {
    strcpy(oldValue.value, newValue.value);
}

bool test(void) {
    bool testResult = true;
    AVLTree *testAvlTree = createAVLTree();
    Type value = {0};
    Type zeroValue = {0};
    int errorCode = 0;

    strcpy(value.key, "asd");
    strcpy(value.value, "asd");
    addValue(testAvlTree, value, &compare, &whatToDoIfEqualInAdding);

    strcpy(value.key, "bds");
    strcpy(value.value, "asx");
    addValue(testAvlTree, value, &compare, &whatToDoIfEqualInAdding);

    testResult = !strcmp("asx", findValue(testAvlTree, value, &errorCode, zeroValue, &compare).value);
    if (errorCode) {
        return false;
    }

    strcpy(value.key, "asd");
    testResult = testResult && !strcmp("asd", deleteValue(testAvlTree, value, compare).value);

    testResult = testResult && !strcmp(zeroValue.value, findValue(testAvlTree, value, &errorCode, zeroValue, &compare).value);
    if (errorCode) {
        return false;
    }

    strcpy(value.key, "bds");
    testResult = testResult && !strcmp("asx", findValue(testAvlTree, value, &errorCode, zeroValue, &compare).value);
    if (errorCode) {
        return false;
    }

    return testResult;
}

int main(void) {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");

    AVLTree *avlTree = createAVLTree();

    while (true) {
        int userInput = 0;
        printf("0 – выйти\n"
               "1 – Добавить значение по заданному ключу в словарь. Если такой ключ уже есть, значение заменяется на новое\n"
               "2 – Получить значение по заданному ключу из словаря. Если такого ключа нет, возвращается NULL\n"
               "3 – Проверить наличие заданного ключа\n"
               "4 - Удалить заданный ключ и связанное с ним значение из словаря\n");
        scanf("%d", &userInput);
        bool endCondition = false;
        switch (userInput) {
            case 0: {
                endCondition = true;
                break;
            } case 1: {
                printf("Введите сначала ключ, а потом значение элемента, который хотите добавить в авл дерево\n");
                char inputValue[MAX_STRING_SIZE] = {0};
                char inputKey[MAX_STRING_SIZE] = {0};
                scanf("%s", inputKey);
                scanf("%s", inputValue);
                Type value = {0};
                strcpy(value.key, inputKey);
                strcpy(value.value, inputValue);
                if (addValue(avlTree, value, &compare, &whatToDoIfEqualInAdding)) {
                    printf("Возникла ошибка\n");
                }
                break;
            } case 2: {
                printf("Введите ключ элемента, значение которого хотите найти в авл дереве\n");
                char inputKey[MAX_STRING_SIZE] = {0};
                scanf("%s", inputKey);
                Type elementContainingTheKey = {0};
                Type zeroValue = {0};

                strcpy(elementContainingTheKey.key, inputKey);
                int errorCode = 0;
                Type elementContainingTheValue = findValue(avlTree, elementContainingTheKey, &errorCode, zeroValue, &compare);
                if (errorCode) {
                    printf("Возникла ошибка\n");
                } else {
                    if (!strcmp(elementContainingTheValue.value, zeroValue.value)) {
                        printf("NULL\n");
                    } else {
                        printf("Вот значение этого элемента: %s\n", elementContainingTheValue.value);
                    }
                }
                break;
            } case 3: {
                printf("Введите ключ элемента, наличие которого хотите проверить в авл дереве\n");
                char inputKey[MAX_STRING_SIZE] = {0};
                scanf("%s", inputKey);
                Type elementContainingTheKey = {0};
                Type zeroValue = {0};
                strcpy(elementContainingTheKey.key, inputKey);
                int errorCode = 0;
                Type elementContainingTheValue = findValue(avlTree, elementContainingTheKey, &errorCode, zeroValue, &compare);
                if (errorCode) {
                    printf("Возникла ошибка\n");
                } else {
                    if (!strcmp(zeroValue.value, elementContainingTheValue.value)) {
                        printf("Элемент с таким ключом нет в авл дереве\n");
                    } else {
                        printf("Элемента с таким ключом есть в авл дереве\n");
                    }
                }
                break;
            } case 4: {
                printf("Введите ключ элемента, который вы хотите удалить\n");
                char inputKey[MAX_STRING_SIZE] = {0};
                scanf("%s", inputKey);
                Type value = {0};
                strcpy(value.key, inputKey);
                deleteValue(avlTree, value, &compare);
                break;
            } default: {
                printf("Неизвестный ввод - повторите попытку\n");
            }
        }

        if (endCondition) {
            break;
        }
    }

    return 0;
}