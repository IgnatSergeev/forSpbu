#include <stdbool.h>
#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include "../Modules/ListModule/customList.h"

bool isDigit(char symbol) {
    return symbol >= '0' && symbol <= '9';
}

int readPhoneBookFromFile(FILE *file, List *phoneBook) {
    char currentName[MAX_LINE_SIZE] = {0};
    char currentPhoneNumber[MAX_PHONE_NUMBER_SIZE] = {0};

    int phoneBookIndex = 0;
    while (fscanf(file, "%s", currentName) == 1) {
        fscanf(file, "%s", currentPhoneNumber);
        if (currentPhoneNumber[0] !='+' || currentPhoneNumber[1] != '7') {
            return -1;
        }
        for (int i = 2; i < MAX_PHONE_NUMBER_SIZE - 1; i++) {
            if (!isDigit(currentPhoneNumber[i])) {
                return -1;
            }
        }

        Type contact;
        strcpy(contact.name, currentName);
        strcpy(contact.phoneNumber, currentPhoneNumber);

        insertNode(phoneBook, contact, phoneBookIndex);
        ++phoneBookIndex;
    }

    return 0;
}

void printNode(Type value) {
    printf("%s - %s\n", value.name, value.phoneNumber);
}

int compareNames(Type value1, Type value2) {
    int compareResult = strcmp(value1.name, value2.name);
    return (compareResult > 0) ? 1 : (compareResult < 0) ? -1 : 0;
}

int comparePhoneNumbers(Type value1, Type value2) {
    int compareResult = strcmp(value1.phoneNumber, value2.phoneNumber);
    return (compareResult > 0) ? 1 : (compareResult < 0) ? -1 : 0;
}

bool test() {
    bool testResult = true;

    List *phoneBook = create();
    if (phoneBook == NULL) {
        printf("Проблемы с созданием тестовой книги с кнотактами\n");

        return false;
    }
    FILE *file = fopen("../19.10/mergeSortListTest.txt", "r");
    if (file == NULL) {
        printf("Тестовый файл не найден\n");

        clear(phoneBook);
        return false;
    }

    if (readPhoneBookFromFile(file, phoneBook)) {
        printf("Тестовый файл изменён (неверный формат телефона)\n");

        clear(phoneBook);
        fclose(file);
        return false;
    }
    fclose(file);

    Type zeroValue;
    strcpy(zeroValue.name, "");
    strcpy(zeroValue.phoneNumber, "");
    int errorCode = 0;
    Type firstNodeBeforeSort = findNode(phoneBook, 0, zeroValue, &errorCode);
    if (errorCode) {
        clear(phoneBook);
        return false;
    }
    Type secondNodeBeforeSort = findNode(phoneBook, 1, zeroValue, &errorCode);
    if (errorCode) {
        clear(phoneBook);
        return false;
    }

    if (strcmp(firstNodeBeforeSort.phoneNumber, "+79219210202") != 0 || strcmp(firstNodeBeforeSort.name, "person2") != 0) {
        clear(phoneBook);
        return false;
    }
    if (strcmp(secondNodeBeforeSort.phoneNumber, "+72212323434") != 0 || strcmp(secondNodeBeforeSort.name, "person1") != 0) {
        clear(phoneBook);
        return false;
    }

    if (mergeSort(phoneBook, &compareNames)) {
        clear(phoneBook);
        return false;
    }

    Type firstNodeAfterNameSort = findNode(phoneBook, 0, zeroValue, &errorCode);
    if (errorCode) {
        clear(phoneBook);
        return false;
    }
    Type secondNodeAfterNameSort = findNode(phoneBook, 1, zeroValue, &errorCode);
    if (errorCode) {
        clear(phoneBook);
        return false;
    }

    if (strcmp(firstNodeAfterNameSort.phoneNumber, "+72212323434") != 0 || strcmp(firstNodeAfterNameSort.name, "person1") != 0) {
        clear(phoneBook);
        return false;
    }
    if (strcmp(secondNodeAfterNameSort.phoneNumber, "+79219210202") != 0 || strcmp(secondNodeAfterNameSort.name, "person2") != 0) {
        clear(phoneBook);
        return false;
    }

    if (mergeSort(phoneBook, &comparePhoneNumbers)) {
        clear(phoneBook);
        return false;
    }

    Type firstNodeAfterPhoneNumberSort = findNode(phoneBook, 0, zeroValue, &errorCode);
    if (errorCode) {
        clear(phoneBook);
        return false;
    }
    Type secondNodeAfterPhoneNumberSort = findNode(phoneBook, 1, zeroValue, &errorCode);
    if (errorCode) {
        clear(phoneBook);
        return false;
    }

    if (strcmp(firstNodeAfterPhoneNumberSort.phoneNumber, "+72212323434") != 0 || strcmp(firstNodeAfterPhoneNumberSort.name, "person1") != 0) {
        clear(phoneBook);
        return false;
    }
    if (strcmp(secondNodeAfterPhoneNumberSort.phoneNumber, "+79219210202") != 0 || strcmp(secondNodeAfterPhoneNumberSort.name, "person2") != 0) {
        clear(phoneBook);
        return false;
    }
    clear(phoneBook);
    return true;
}

int main() {
    if (!test()) {
        printf("Тесты провалены\n");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Программа прочитает контакты из файла mergeSortList.txt и отсортирует их по вашему выбору\n");
    FILE *file = fopen("../19.10/mergeSortList.txt", "r");
    if (file == NULL) {
        printf("Файл не найден");

        return -1;
    }

    List *phoneBook = create();
    if (phoneBook == NULL) {
        printf("Проблемы с созданием тестовой книги с кнотактами\n");

        fclose(file);
        return -1;
    }
    if (readPhoneBookFromFile(file, phoneBook)) {
        printf("Во входном файле найден не правильный формат телефона\n");

        clear(phoneBook);
        fclose(file);
        return -1;
    }
    fclose(file);

    int userInput = 0;
    printf("1 - отсортировать записи по имени\n"
           "2 - отсортировать записи по телефону\n");
    scanf("%d", &userInput);

    switch (userInput) {
        case 1: {
            if (mergeSort(phoneBook, &compareNames)) {
                printf("Книга с записями пуста");

                clear(phoneBook);
                return -1;
            }
            print(phoneBook, &printNode);

            break;
        }
        case 2: {
            if (mergeSort(phoneBook, &comparePhoneNumbers)) {
                printf("Книга с записями пуста");

                clear(phoneBook);
                return -1;
            }
            print(phoneBook, &printNode);

            break;
        }
        default: {
            printf("Неизвестный ввод\n");
            break;
        }
    }

    clear(phoneBook);
    return 0;
}