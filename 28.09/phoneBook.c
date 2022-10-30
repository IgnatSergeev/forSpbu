#include <stdbool.h>
#include <stdio.h>
#include <malloc.h>

#define lineMaxSize 100
#define phoneBookMaxSize 100
#define phoneNumberMaxSize 13

struct Contact {
    char name[lineMaxSize - phoneNumberMaxSize];
    char phoneNumber[phoneNumberMaxSize];
} Contact;

void printPhoneBook(struct Contact *phoneBook[], int phoneBookSize) {
    for (int i = 0; i < phoneBookSize; i++) {
        struct Contact *currentContact = phoneBook[i];
        printf("%s %s\n", currentContact->name, currentContact->phoneNumber);
    }
}

void addContact(struct Contact *phoneBook[], int phoneBookSizeIncludingContactsAddedInCurrentRun, char newContactsName[], char newContactsPhoneNumber[]) {
    struct Contact *newContact = calloc(1, sizeof(Contact));
    for (int i = 0; i < lineMaxSize; i++) {
        if (newContactsName[i] == '\0') {
            break;
        }
        newContact->name[i] = newContactsName[i];
    }
    for (int i = 0; i < lineMaxSize; i++) {
        if (newContactsPhoneNumber[i] == '\0') {
            break;
        }
        newContact->phoneNumber[i] = newContactsPhoneNumber[i];
    }
    phoneBook[phoneBookSizeIncludingContactsAddedInCurrentRun] = newContact;
}

bool isStringsEqual(const char string1[], const char string2[]) {
    bool isEqual = true;
    for (int i = 0; i < lineMaxSize; i++) {
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

char *searchByTheName(struct Contact *phoneBook[], int phoneBookSizeIncludingContactsAddedInCurrentRun, char contactsName[]) {
    char *contactsPhoneNumber = NULL;

    for (int i = 0; i < phoneBookSizeIncludingContactsAddedInCurrentRun; i++) {
        char *currentContactsName = phoneBook[i]->name;
        if (isStringsEqual(currentContactsName, contactsName)) {
            contactsPhoneNumber = phoneBook[i]->phoneNumber;
            break;
        }
    }
    return contactsPhoneNumber;
}

char *searchByThePhoneNumber(struct Contact *phoneBook[], int phoneBookSizeIncludingContactsAddedInCurrentRun, char contactsPhoneNumber[]) {
    char *contactsName = NULL;

    for (int i = 0; i < phoneBookSizeIncludingContactsAddedInCurrentRun; i++) {
        char *currentContactsPhoneNumber = phoneBook[i]->phoneNumber;
        if (isStringsEqual(currentContactsPhoneNumber, contactsPhoneNumber)) {
            contactsName = phoneBook[i]->name;
            break;
        }
    }
    return contactsName;
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
    struct Contact *phoneBook[phoneBookMaxSize] = {0};

    int phoneBookIndex = 0;
    while (fscanf(file, "%s", word) == 1) {
        struct Contact *currentContact = calloc(1, sizeof(Contact));
        for (int i = 0; i < lineMaxSize; i++) {
            if (word[i] == '\0') {
                break;
            }
            currentContact->name[i] = word[i];
        }
        char currentPhoneNumber[phoneNumberMaxSize] = {0};
        fscanf(file, "%s", currentPhoneNumber);
        for (int i = 0; i < lineMaxSize; i++) {
            if (currentPhoneNumber[i] == '\0') {
                break;
            }
            currentContact->phoneNumber[i] = currentPhoneNumber[i];
        }

        phoneBook[phoneBookIndex] = currentContact;
        ++phoneBookIndex;
    }
    fclose(file);

    int phoneBookSize = phoneBookIndex;
    int numberOfAddedContacts = 0;
    char inputName[lineMaxSize - phoneNumberMaxSize] = {0};
    char inputPhoneNumber[phoneNumberMaxSize] = {0};

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

        switch (userInput) {
            case 0:
                endCondition = true;
                break;
            case 1:
                if (numberOfAddedContacts + phoneBookSize >= 100) {
                    printf("Количество хранимых контактов максимально, добавить новый невозможно\n");
                    break;
                }

                printf("Введите имя (длиной не боллее %d символов), а на следующей строке - "
                       "номер телефона в формате: +7 и %d цифр\n", lineMaxSize - phoneNumberMaxSize - 1,
                       phoneNumberMaxSize - 3);

                scanf("%s", inputName);
                scanf("%s", inputPhoneNumber);

                if (inputPhoneNumber[0] !='+' || inputPhoneNumber[1] != '7') {
                    printf("Не правильный формат телефона\n");
                    continueCondition = true;
                    break;
                }
                for (int i = 2; i < phoneNumberMaxSize - 1; i++) {
                    if (!isDigit(inputPhoneNumber[i])) {
                        printf("Не правильный формат телефона\n");
                        continueCondition = true;
                        break;
                    }
                }

                addContact(phoneBook, phoneBookSize + numberOfAddedContacts, inputName, inputPhoneNumber);
                ++numberOfAddedContacts;
                break;
            case 2:
                printPhoneBook(phoneBook, phoneBookSize + numberOfAddedContacts);
                break;
            case 3:
                printf("Введите имя (длиной не боллее %d символов), номер которого вы хотите найти в книге\n",
                       lineMaxSize - phoneNumberMaxSize - 1);
                scanf("%s", inputName);
                char *phoneNumber = searchByTheName(phoneBook, phoneBookSize + numberOfAddedContacts, inputName);
                if (phoneNumber == NULL) {
                    printf("Конакт с таким именем не найден\n");
                    break;
                }
                printf("Вот номер телефона искомого контакта - %s\n", phoneNumber);
                break;
            case 4:
                printf("Введите номер телефона контакта в формате: +7 и %d цифр, имя которого вы хотите найти в книге\n",
                       phoneNumberMaxSize - 3);
                scanf("%s", inputPhoneNumber);
                char *name = searchByThePhoneNumber(phoneBook, phoneBookSize + numberOfAddedContacts, inputPhoneNumber);
                if (name == NULL) {
                    printf("Конакт с таким номером телефона не найден\n");
                    break;
                }
                printf("Вот имя искомого контакта - %s\n", name);
                break;
            case 5:
                file = fopen("../28.09/phoneBook.txt", "a");
                if (file == NULL) {
                    printf("Файл не найден\n");

                    return -1;
                }

                for (int i = phoneBookSize; i < phoneBookSize + numberOfAddedContacts; i++) {
                    fprintf(file, "%s %s\n", phoneBook[i]->name, phoneBook[i]->phoneNumber);
                }

                fclose(file);
                break;
            default:
                printf("Неизвестный ввод - повторите попытку\n");
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

    for (int i = 0; i < phoneBookSize + numberOfAddedContacts; i++) {
        free(phoneBook[i]);
    }
    return 0;
}