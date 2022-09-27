#include <stdio.h>
#include <stdbool.h>
#include <math.h>

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

bool tester() {
    bool typicalTest = (recursiveFibonacci(12) == 233) && (iterativeFibonacci(12) == 233);
    bool incorrectInputTest = (recursiveFibonacci(-5) == 1) && (iterativeFibonacci(-5) == 1);

    return typicalTest && incorrectInputTest;
}

int main() {
    if (!tester()) {
        printf("Tests failed");

        return 0;
    } else {
        printf("Tests passed\n");
    }

    int inputIndex = 0;

    printf("%s", "Enter the index of the fibonacci number you want to know\n");
    scanf("%d", &inputIndex);

    printf("Here is the result of the recursive algorithm:\n%d\n", recursiveFibonacci(inputIndex));
    printf("Here is the result of the iterative algorithm:\n%d\n", iterativeFibonacci(inputIndex));

    int numberOfOperationsByRecursion = 0;
    int numberOfOperationsByIterativeAlgorithm = 0;
    int indexOfFibonacciNumberWeWantToCompare = 0;
    while (numberOfOperationsByRecursion <= numberOfOperationsByIterativeAlgorithm) {
        indexOfFibonacciNumberWeWantToCompare += 1;

        numberOfOperationsByRecursion = (int)pow(2, indexOfFibonacciNumberWeWantToCompare);
        numberOfOperationsByIterativeAlgorithm = indexOfFibonacciNumberWeWantToCompare;
    }
    printf("Here is the index starting from which iterative algorithm is facter:\n%d", indexOfFibonacciNumberWeWantToCompare);

    return 0;
}
