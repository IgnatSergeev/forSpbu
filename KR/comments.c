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
    char line[101] = {0};
    while (!feof(file)) {
        if (fgets((char *) &line, 100, file) == NULL) {
            printf("Возникла ошибка при чтении\n");

            return -1;
        }
        printf(line);
    }

}