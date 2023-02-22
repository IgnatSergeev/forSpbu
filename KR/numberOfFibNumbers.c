#include <stdio.h>
#include <stdbool.h>
#include <locale.h>

int iterativeFibonacci(int fibonacciIndex) {
    if (fibonacciIndex < 0) {
        return -1;
    }

    int arrayOf3Fibonacci[3] = {0, 1, 1};

    for (int i = 1; i < fibonacciIndex; i++) {
        int previousSecond = arrayOf3Fibonacci[2];
        arrayOf3Fibonacci[2] = arrayOf3Fibonacci[2] + arrayOf3Fibonacci[1];
        arrayOf3Fibonacci[0] = arrayOf3Fibonacci[1];
        arrayOf3Fibonacci[1] = previousSecond;
    }

    return arrayOf3Fibonacci[2];
}

bool test() {
    bool typicalTest = iterativeFibonacci(12) == 233;
    bool incorrectInputTest = iterativeFibonacci(-5) == -1;

    return typicalTest && incorrectInputTest;
}

int main() {
    setlocale(LC_ALL, "");

    if (!test()) {
        printf("Тесты провалены");

        return -1;
    } else {
        printf("Тесты пройдены\n");
    }

    int curIndex = 1;
    int curFibNumber = iterativeFibonacci(curIndex);
    int sumOfEvenFibNumbers = 0;
    while (curFibNumber <= 1000000) {
        if (curFibNumber % 2 == 0) {
            sumOfEvenFibNumbers += curFibNumber;
        }
        ++curIndex;
        curFibNumber = iterativeFibonacci(curIndex);
    }

    printf("Сумма чётных чисел фибоначи = %d", sumOfEvenFibNumbers);

    return 0;
}
