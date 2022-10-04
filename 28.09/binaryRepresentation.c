#include <stdio.h>
#include <stdbool.h>
#include <malloc.h>
#include <time.h>

bool test() {
    bool typicalTest = true;

    return typicalTest;
}

int main() {
    if (!test()) {
        printf("Tests failed");

        return -1;
    } else {
        printf("Tests passed\n");
    }

    int firstNumber = 0;
    int secondNumber = 0;
    printf("%s", "Enter the numbers\n");
    scanf("%d", &firstNumber);
    scanf("%d", &secondNumber);

    return 0;
}