#include <stdbool.h>
#include <stdio.h>
#include <string.h>

bool test() {
    bool testResult = true;

    return testResult;
}

int main() {
    if (!test()) {
        printf("Тесты провалены\n");
        return -1;
    } else {
        printf("Тесты пройдены\n");
    }


    return 0;
}