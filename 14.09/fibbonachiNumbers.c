#include <stdio.h>
#include <stdbool.h>
#include <math.h>
#include <time.h>

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
    int startRecursiveFibonacciTime = clock();
    recursiveFibonacci();
    int endRecursiveFibonacciTime = clock();
    int recursiveFibonacciTime  = endRecursiveFibonacciTime - startRecursiveFibonacciTime;

    int startIterativeFibonacciTime = clock();
    iterativeFibonacci();
    int endIterativeFibonacciTime = clock();
    int iterativeFibonacciTime  = endIterativeFibonacciTime - startIterativeFibonacciTime;

    printf("Recursive fibonacci time: %d\nIterative fibonacci time: %d",recursiveFibonacciTime, iterativeFibonacciTime);
}

int main() {
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
