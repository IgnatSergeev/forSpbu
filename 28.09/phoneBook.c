#include <stdbool.h>
#include <stdio.h>

struct Contact {
    char *name;
    char *phoneNumber;
};

bool test() {
    bool typicalTest = true;

    return typicalTest;
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
    char line[100] = {0};
    while (fscanf(file, "%s", line) == 1) {

    }
}