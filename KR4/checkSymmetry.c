#include "../Modules/ListModule/customList.h"
#include "../Modules/customStack.h"
#include <stdio.h>

bool checkSymmetry(List *list, int listSize, int *errorCode) {
    *errorCode = 0;
    Stack *stack = createStack();

    int listIndex = 0;
    if (listSize % 2 == 0) {
        while (listIndex < listSize / 2) {
            int currentNumber = findNode(list, listIndex, errorCode);
            if (*errorCode) {
                deleteStack(stack);
                return false;
            }
            *errorCode = push(stack, currentNumber);
            if (*errorCode) {
                deleteStack(stack);
                return false;
            }
            ++listIndex;
        }
        while (listIndex < listSize) {
            int currentNumber = findNode(list, listIndex, errorCode);
            if (*errorCode) {
                deleteStack(stack);
                return false;
            }

            int popResultCode = 0;
            int popResult = pop(stack, &popResultCode);
            if (popResultCode) {
                deleteStack(stack);
                return false;
            }
            if (popResult != currentNumber) {
                deleteStack(stack);
                return false;
            }
            ++listIndex;
        }

        deleteStack(stack);
        return true;
    }

    while (listIndex < (listSize - 1) / 2) {
        int currentNumber = findNode(list, listIndex, errorCode);
        if (*errorCode) {
            deleteStack(stack);
            return false;
        }
        *errorCode = push(stack, currentNumber);
        if (*errorCode) {
            deleteStack(stack);
            return false;
        }
        ++listIndex;
    }

    ++listIndex;
    while (listIndex < listSize) {
        int currentNumber = findNode(list, listIndex, errorCode);
        if (*errorCode) {
            deleteStack(stack);
            return false;
        }

        int popResultCode = 0;
        int popResult = pop(stack, &popResultCode);
        if (popResultCode) {
            deleteStack(stack);
            return false;
        }
        if (popResult != currentNumber) {
            deleteStack(stack);
            return false;
        }
        ++listIndex;
    }

    deleteStack(stack);
    return true;
}

bool test() {
    bool testResult = true;
    List *list = create();
    if (list == NULL) {
        return false;
    }

    int errorCode = 0;
    insertNode(list, 1, 0);
    testResult = checkSymmetry(list, 1, &errorCode);
    if (errorCode) {
        clear(list);
        return false;
    }

    insertNode(list, 1, 1);
    testResult = testResult && checkSymmetry(list, 2, &errorCode);
    if (errorCode) {
        clear(list);
        return false;
    }

    insertNode(list, 2, 2);
    testResult = testResult && !checkSymmetry(list, 3, &errorCode);
    if (errorCode) {
        clear(list);
        return false;
    }

    deleteNode(list, 2);
    insertNode(list, 1, 2);
    testResult = testResult && checkSymmetry(list, 3, &errorCode);
    if (errorCode) {
        clear(list);
        return false;
    }

    insertNode(list, 1, 3);
    testResult = testResult && checkSymmetry(list, 4, &errorCode);
    if (errorCode) {
        clear(list);
        return false;
    }

    clear(list);
    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    } else {
        printf("Тесты пройдены\n");
    }


    List *list = create();
    if (list == NULL) {
        printf("Возникла проблема с создание списка\n");
        return -1;
    }

    printf("Введите последовательность чисел, которую хотите проверить на симметричность в файл checkSymmetry.txt\n");
    FILE *file = fopen("../KR4/checkSymmetry.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");
        clear(list);
        return -1;
    }
    int currentNumber = 0;
    int listIndex = 0;
    while (fscanf(file, "%d", &currentNumber) != -1) {
        insertNode(list, currentNumber, listIndex);
        ++listIndex;
    }
    fclose(file);

    int listSize = listIndex;
    if (listSize == 0) {
        printf("В файле не найдено ни одного числа, проверьте корректность введённых данных\n");
        clear(list);
        return -1;
    }

    int errorCode = 0;
    bool checkResult = checkSymmetry(list, listSize, &errorCode);
    if (errorCode) {
        printf("Возникла ошибка при проверке на симметричность, проверьте корректность введённых данных\n");
        clear(list);
        return -1;
    }

    if (checkResult) {
        printf("Данная последовательность чисел является симметричной\n");
        clear(list);
        return 0;
    }

    printf("Данная последовательность чисел НЕ является симметричной\n");
    clear(list);
    return 0;
}