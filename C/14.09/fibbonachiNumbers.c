#include <stdio.h>
#include <stdbool.h>
#include <math.h>
#include <time.h>
#include <locale.h>

int recursiveFibonacci(int fibonacciIndex) {
    if (fibonacciIndex <= 1) {
        return 1;
    }
    return recursiveFibonacci(fibonacciIndex - 1) + recursiveFibonacci(fibonacciIndex - 2);
}

int iterativeFibonacci(int fibonacciIndex) {
    if (fibonacciIndex < 0) {
        return 1;
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
    bool typicalTest = (recursiveFibonacci(12) == 233) && (iterativeFibonacci(12) == 233);
    bool incorrectInputTest = (recursiveFibonacci(-5) == 1) && (iterativeFibonacci(-5) == 1);

    return typicalTest && incorrectInputTest;
}

void timeTest() {
    for (int i = 0; i <= 1000; i++) {
        int startRecursiveFibonacciTime = clock();
        recursiveFibonacci(i);
        int endRecursiveFibonacciTime = clock();
        int recursiveFibonacciTime  = endRecursiveFibonacciTime - startRecursiveFibonacciTime;

        int startIterativeFibonacciTime = clock();
        iterativeFibonacci(i);
        int endIterativeFibonacciTime = clock();
        int iterativeFibonacciTime  = endIterativeFibonacciTime - startIterativeFibonacciTime;

        int timeDifference = abs(recursiveFibonacciTime - iterativeFibonacciTime);
        if (timeDifference > CLOCKS_PER_SEC * 2) {
            printf("Индекс, с которого итеративный вариант заметно быстрее рекурсивного = %d", i);
            break;
        }
    }
}

int main() {
    setlocale (LC_ALL, "ru_RU.UTF-8");
    if (!test()) {
        printf("Tests failed");

        return -1;
    } else {
        printf("Tests passed\n");
    }

    int inputIndex = 0;

    printf("%s", "Enter the index of the fibonacci number you want to know\n");
    scanf("%d", &inputIndex);

    printf("Here is the result of the recursive algorithm:\n%d\n", recursiveFibonacci(inputIndex));
    printf("Here is the result of the iterative algorithm:\n%d\n", iterativeFibonacci(inputIndex));

    timeTest();

    return 0;
}
