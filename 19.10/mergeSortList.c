#include <stdbool.h>
#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include "../Modules/ListModule/customList.h"

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

bool isStringsEqual(const char string1[], const char string2[]) {
    bool isEqual = true;
    for (int i = 0; i < MAX_LINE_SIZE; i++) {
        if (string1[i] == '\0') {
            isEqual = string2[i] == '\0';
            break;
        }
        if (string2[i] == '\0') {
            isEqual = string1[i] == '\0';
            break;
        }
        if (string1[i] != string2[i]) {
            isEqual = false;
            break;
        }
    }

    return isEqual;
}

bool isDigit(char symbol) {
    if (symbol == '1' || symbol == '2' || symbol == '3' || symbol == '4' || symbol == '5' || symbol == '6'
        || symbol == '7' || symbol == '8' || symbol == '9' || symbol == '0') {
        return true;
    }
    return false;
}

bool test() {

    return true;
}

int main() {
    if (!test()) {
        printf("Тесты провалены\n");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    FILE *file = fopen("../19.10/mergeSortList.txt", "r");
    if (file == NULL) {
        printf("Файл не найден");

        return -1;
    }

    char currentName[MAX_LINE_SIZE] = {0};
    char currentPhoneNumber[MAX_PHONE_NUMBER_SIZE] = {0};
    List *phoneBook = create();

    int phoneBookIndex = 0;
    while (fscanf(file, "%s", currentName) == 1) {
        fscanf(file, "%s", currentPhoneNumber);
        if (currentPhoneNumber[0] !='+' || currentPhoneNumber[1] != '7') {
            printf("Во входном файле найден не правильный формат телефона\n");

            clear(phoneBook);
            return -1;
        }
        for (int i = 2; i < MAX_PHONE_NUMBER_SIZE - 1; i++) {
            if (!isDigit(currentPhoneNumber[i])) {
                printf("Во входном файле найден не правильный формат телефона\n");

                clear(phoneBook);
                return -1;
            }
        }

        Type contact;
        strcpy(contact.name, currentName);
        strcpy(contact.phoneNumber, currentPhoneNumber);

        insertNode(phoneBook, contact, phoneBookIndex);
        ++phoneBookIndex;
    }
    fclose(file);

    int phoneBookSize = phoneBookIndex;
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