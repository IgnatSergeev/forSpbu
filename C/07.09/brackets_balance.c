#include <stdio.h>

void checkTheSequence(const char bracketsSequence[]) {
    const int maxLengthOfsSequence = 1000;
    int sumOfBrackets = 0;
    for (int i = 0; i < maxLengthOfsSequence; i++) {
        if (bracketsSequence[i] == '\0') {
            if (sumOfBrackets != 0) {
                printf("%s", "Последовательность неправильна");
            } else {
                printf("%s", "Последовательность правильна");
            }
            return;
        }

        if (bracketsSequence[i] == '(') {
            sumOfBrackets += 1;
        } else {
            if (bracketsSequence[i] == ')') {
                sumOfBrackets -= 1;
            } else {
                printf("%s", "Это не последовательность скобок");
                return;
            }
        }

        if (sumOfBrackets < 0) {
            printf("%s", "Последовательность неправильна");
            return;
        }
    }
}

int main() {
    char bracketsSequence[1001] = {0};

    printf("%s", "Введите последовательность скобок, в которой их не более 1000\n");
    scanf("%s", bracketsSequence);

    checkTheSequence(bracketsSequence);

    return 0;
}
