#include <stdio.h>

int calculateIncompleteQuotient(int dividend, int divider, char signOfIncompleteDividend, char signOfIncompleteDivider) {
    int incompleteQuotient = -1;

    while (dividend >= 0) {
        dividend -= divider;
        incompleteQuotient += 1;
    }

    if ((signOfIncompleteDividend == '+') && (signOfIncompleteDivider == '+')) {
        return incompleteQuotient;
    }
    if ((signOfIncompleteDividend == '-') && (signOfIncompleteDivider == '+')) {
        if (dividend == -divider) {
            return -incompleteQuotient;
        } else {
            return -incompleteQuotient - 1;
        }
    }
    if ((signOfIncompleteDividend == '+') && (signOfIncompleteDivider == '-')) {
        return -incompleteQuotient;
    }
    if (dividend == -divider) {
        return incompleteQuotient;
    } else {
        return incompleteQuotient + 1;
    }
}

int main() {
    int dividend = 0;
    int divider = 0;
    char signOfIncompleteDividend = '+';
    char signOfIncompleteDivider = '+';

    printf("%s", "Введите сначала делимое, потом делитель(не равный 0)\n");
    scanf("%d", &dividend);
    scanf("%d", &divider);
    if (divider == 0) {
        printf("%s", "Делитель равен 0");
        return 0;
    }

    if (dividend < 0) {
        dividend *= -1;
        signOfIncompleteDividend = '-';
    }
    if (divider < 0) {
        divider *= -1;
        signOfIncompleteDivider = '-';
    }

    int incompleteQuotient = calculateIncompleteQuotient(dividend, divider, signOfIncompleteDividend, signOfIncompleteDivider);

    printf("Неполное частное = %d", incompleteQuotient);

    return 0;
}
