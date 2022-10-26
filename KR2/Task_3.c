#include <stdio.h>
#include <stdbool.h>
#include <locale.h>
#include <malloc.h>
#include <time.h>

#define NUM_OF_ASCII_CODES 128

bool test() {
    bool testResult = true;

    return testResult;
}

int main() {
    setlocale(LC_ALL, "");

    if (!test()) {
        printf("Тесты провалены\n");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    FILE *file = fopen("../KR2/fileForTask3.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");
        return -1;
    }
    int arrayWithNumbersOfApearences[NUM_OF_ASCII_CODES] = {0};

    while (!feof(file)) {
        char letter = 0;
        fscanf(file, "%c", &letter);
        ++arrayWithNumbersOfApearences[(int)letter];
    }

    printf("Количества появлений каждого из сиволов ASCII в файле: \n");
    for (int i = 0; i < NUM_OF_ASCII_CODES; i++) {
        char curentLetter = (char)i;
        if (i != 0 && i != 7 && i != 8 && i != 9 && i != 10 && i != 11 && i != 12 && i != 13 && i != 32) {
            printf("%c : %d\n", curentLetter, arrayWithNumbersOfApearences[i]);
            continue;
        }
        if (i == 0) {
            printf("NUL : %d\n", arrayWithNumbersOfApearences[i]);
            continue;
        }
        if (i == 7) {
            printf("BEL : %d\n", arrayWithNumbersOfApearences[i]);
            continue;
        }
        if (i == 8) {
            printf("BS : %d\n", arrayWithNumbersOfApearences[i]);
            continue;
        }
        if (i == 9) {
            printf("TAB : %d\n", arrayWithNumbersOfApearences[i]);
            continue;
        }
        if (i == 10) {
            printf("LF(\\n) : %d\n", arrayWithNumbersOfApearences[i]);
            continue;
        }
        if (i == 11) {
            printf("VT : %d\n", arrayWithNumbersOfApearences[i]);
            continue;
        }
        if (i == 12) {
            printf("FF : %d\n", arrayWithNumbersOfApearences[i]);
            continue;
        }
        if (i == 13) {
            printf("CR : %d\n", arrayWithNumbersOfApearences[i]);
            continue;
        }
        printf("SPACE : %d\n", arrayWithNumbersOfApearences[i]);
    }
    fclose(file);
    return 0;
}
