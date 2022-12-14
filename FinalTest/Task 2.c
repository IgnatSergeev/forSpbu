#include <stdbool.h>
#include <stdio.h>
#include <string.h>
#include "../Modules/ListModule/customList.h"

bool test() {
    bool testResult = true;
    List *list = create();
    if (list == NULL) {
        printf("Ошибка при создании списка\n");
        return false;
    }

    int errorCode =insertNodeToEnd(list, "abc");
    if (errorCode) {
        printf("Возникла ошибка при добавлении ноды\n");
        return false;
    }
    errorCode = insertNodeToEnd(list, "");
    if (errorCode) {
        printf("Возникла ошибка при добавлении ноды\n");
        return false;
    }
    errorCode = insertNodeToEnd(list, "acd");
    if (errorCode) {
        printf("Возникла ошибка при добавлении ноды\n");
        return false;
    }
    errorCode = insertNodeToEnd(list, "bcd");
    if (errorCode) {
        printf("Возникла ошибка при добавлении ноды\n");
        return false;
    }

    errorCode = appendToEndStringsStartingWithA(list);
    if (errorCode) {
        printf("Возникла ошибка в вызове функции\n");
        return false;
    }

    Type value = deleteLastNode(list);
    if (value == NULL) {
        printf("Возникла ошибка при удалении ноды\n");
        return false;
    }
    if (strcmp(value, "acd")) {
        testResult = false;
    }

    value = deleteLastNode(list);
    if (value == NULL) {
        printf("Возникла ошибка при удалении ноды\n");
        return false;
    }
    if (strcmp(value, "abc")) {
        testResult = false;
    }

    value = deleteLastNode(list);
    if (value == NULL) {
        printf("Возникла ошибка при удалении ноды\n");
        return false;
    }
    if (strcmp(value, "bcd")) {
        testResult = false;
    }

    value = deleteLastNode(list);
    if (value == NULL) {
        printf("Возникла ошибка при удалении ноды\n");
        return false;
    }
    if (strcmp(value, "acd")) {
        testResult = false;
    }

    value = deleteLastNode(list);
    if (value == NULL) {
        printf("Возникла ошибка при удалении ноды\n");
        return false;
    }
    if (strcmp(value, "")) {
        testResult = false;
    }

    value = deleteLastNode(list);
    if (value == NULL) {
        printf("Возникла ошибка при удалении ноды\n");
        return false;
    }
    if (strcmp(value, "abc")) {
        testResult = false;
    }
    clear(list);
    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    }
    printf("Тесты пройдены\n");
    return 0;
}