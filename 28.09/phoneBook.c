#include <stdbool.h>
#include <stdio.h>

#define lineMaxSize 100
#define phoneBookMaxSize 100
#define phoneNumberMaxSize 13

struct Contact {
    char *name;
    char *phoneNumber;
} Contact;

void printPhoneBook(struct Contact phoneBook[], int phoneBookSize) {
    for (int i = 0; i < phoneBookSize; i++) {
        struct Contact currentContact = phoneBook[i];
        printf("%s %s", currentContact.name, currentContact.phoneNumber);
    }
}

void addContact(struct Contact phoneBook[], int phoneBookSize, char newContactsName[], char newContactsPhoneNumber[]) {

}

bool isDigit(char symbol) {
    if (symbol == '1' || symbol == '2' || symbol == '3' || symbol == '4' || symbol == '5' || symbol == '6'
    || symbol == '7' || symbol == '8' || symbol == '9' || symbol == '0') {
        return true;
    }
    return false;
}

bool test() {
    bool testResult = true;

    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены\n");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    FILE *file = fopen("../28.09/phoneBook.txt", "r");
    if (file == NULL) {
        printf("Файл не найден");

        return -1;
    }

    char word[lineMaxSize] = {0};
    struct Contact phoneBook[phoneBookMaxSize] = {0};

    int phoneBookIndex = 0;
    while (fscanf(file, "%s", word) == 1) {
        char currentName[lineMaxSize] = {0};
        for (int i = 0; i < lineMaxSize; i++) {
            if (word[i] == '\0') {
                break;
            }
            currentName[i] = word[i];
        }
        char currentPhoneNumber[phoneNumberMaxSize] = {0};
        fscanf(file, "%s", currentPhoneNumber);

        struct Contact currentContact = {currentName, currentPhoneNumber};
        phoneBook[phoneBookIndex] = currentContact;
        ++phoneBookIndex;
    }
    int phoneBookSize = phoneBookIndex;
    printPhoneBook(phoneBook, phoneBookSize);
    int numberOfAddedContacts = 0;

    while (true) {
        int userInput = 0;
        printf("0 - выйти\n"
               "1 - добавить запись (имя и телефон)\n"
               "2 - распечатать все имеющиеся записи\n"
               "3 - найти телефон по имени\n"
               "4 - найти имя по телефону\n"
               "5 - сохранить текущие данные в файл\n");
        scanf("%d", &userInput);
        bool endCondition = false;
        bool continueCondition = false;
        char inputName[lineMaxSize - phoneNumberMaxSize] = {0};
        char inputPhoneNumber[phoneNumberMaxSize] = {0};

        switch (userInput) {
            case 0:
                endCondition = true;
                break;
            case 1:
                if (numberOfAddedContacts + phoneBookSize >= 100) {
                    printf("Количество хранимых контактов максимально, добавить новый невозможно");
                    break;
                }

                printf("Введите имя (длиной не боллее %d символов), а на следующей строке - "
                       "номер телефона в формате: +7 и %d цифр\n", lineMaxSize - phoneNumberMaxSize - 1,
                       phoneNumberMaxSize - 3);
                scanf("%s", inputName);
                scanf("%s", inputPhoneNumber);
                for (int i = 0; i < lineMaxSize - phoneNumberMaxSize; i++) {
                    if (inputName[i] == ' ') {
                        printf("В имени не должны содержаться пробелы");
                        continueCondition = true;
                        break;
                    }
                }
                if (inputPhoneNumber[0] !='+' || inputPhoneNumber[1] != '7') {
                    printf("Не правильный формат телефона");
                    continueCondition = true;
                    break;
                }
                for (int i = 2; i < phoneNumberMaxSize; i++) {
                    if (!isDigit(inputPhoneNumber)) {
                        printf("Не правильный формат телефона");
                        continueCondition = true;
                        break;
                    }
                }

                addContact(phoneBook, phoneBookSize, inputName, inputPhoneNumber);
                ++numberOfAddedContacts;
                break;
            case 2:
                printPhoneBook(phoneBook, phoneBookSize);
                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            default:
                printf("Неизвестный ввод - повторите попытку");
                continueCondition = true;
                break;
        }
        if (continueCondition) {
            continue;
        }
        if (endCondition) {
            break;
        }
    }
}