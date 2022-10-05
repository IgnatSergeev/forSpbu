#include <stdio.h>
#include <stdbool.h>
#include <locale.h>
#include <malloc.h>

bool test() {
    bool typicalTest = true;


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

    const int lineSize = 100;
    char line[100] = {0};
    while (!feof(file)) {
        if (fgets((char *) &line, 100, file) == NULL) {
            printf("Возникла ошибка при чтении\n");

            return -1;
        }

        int curIndex = 0;
        while (curIndex < lineSize && line[curIndex] != '\0' && line[curIndex] != ';') {
            ++curIndex;
        }
        if (curIndex < lineSize && line[curIndex] != '\0') {
            for (int i = curIndex; i < lineSize; i++) {
                if (line[i] == '\0') {
                    break;
                }

                printf("%c", line[i]);
            }
        }
    }
}