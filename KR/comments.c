#define lineSize 100
#include <stdio.h>
#include <stdbool.h>
#include <locale.h>
#include <malloc.h>

void printCommentInCommentLine(const char line[], char commentInLine[]) {
    int curIndexInCommentLine = 0;
    int curIndexInInputLine = 0;
    while (curIndexInInputLine < lineSize && line[curIndexInInputLine] != '\0' && line[curIndexInInputLine] != ';') {
        ++curIndexInInputLine;
    }
    if (curIndexInInputLine < lineSize && line[curIndexInInputLine] != '\0') {
        for (int i = curIndexInInputLine; i < lineSize; i++) {
            if (line[i] == '\0') {
                break;
            }

            commentInLine[curIndexInCommentLine] = line[i];
            ++curIndexInCommentLine;
        }
    }
}

bool test() {
    bool typicalTest = true;
    char line[lineSize] = "adcd;232a";
    char commentInLine[lineSize] = {0};
    char correctCommentInLine[lineSize] = ";232a";
    printCommentInCommentLine(line, commentInLine);

    for (int i = 0; i < lineSize; i++) {
        if (commentInLine[i] == '\0') {
            if (correctCommentInLine[i] != '\0') {
                typicalTest = false;
            }
            break;
        }
        if (correctCommentInLine[i] == '\0') {
            typicalTest = false;
            break;
        }

        if (correctCommentInLine[i] != commentInLine[i]) {
            typicalTest = false;
            break;
        }
    }

    return typicalTest;
}

int main() {
    setlocale(LC_ALL, "");

    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    printf("Введите в файл нужный код с комментариями\n");
    FILE *file = fopen("../KR/input.txt", "r");
    if (file == NULL) {
        printf("Файл не найден\n");

        return -1;
    }

    char line[lineSize] = {0};
    while (!feof(file)) {
        if (fgets((char *) &line, 100, file) == NULL) {
            printf("Возникла ошибка при чтении\n");

            return -1;
        }

        char commentInLine[lineSize] = {0};
        printCommentInCommentLine(line, commentInLine);
        for (int i = 0; i < lineSize; i++) {
            if (commentInLine[i] == '\0') {
                break;
            }

            printf("%c", commentInLine[i]);
        }
    }
}