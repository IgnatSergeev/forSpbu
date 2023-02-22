#include <stdio.h>
#include <stdbool.h>

void printAllPrimesLessEqualThanNumber(int number) {
    for (int i = 2; i <= number; i++) {
        bool isPrime = true;
        for (int j = 2; j < i; j++) {
            if (j * j > i) {
                break;
            }
            if (i % j == 0) {
                isPrime = false;
                break;
            }
        }

        if (isPrime && (i == 2)) {
            printf("%d", i);
            continue;
        }
        if (isPrime) {
            printf(", %d", i);
        }
    }
}

int main() {
    int inputNumber = 0;

    printf("%s", "Введите число, для которого нужно найти все простые, меньшие его или равные ему\n");
    scanf("%d", &inputNumber);

    printf("%s", "Вот эти простые числа:\n");
    printAllPrimesLessEqualThanNumber(inputNumber);

    return 0;
}
