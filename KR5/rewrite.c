#include <stdio.h>
#include "../Modules/ListModule/customList.h"

int main(void) {
    if (test()) {
        printf("Тесты пройдены\n");
    } else {
        printf("Тесты провалены\n");
        return -1;
    }

    int leftBorder = 0;
    int rightBorder = 0;
    printf("Введите правую и левую границы отрезка, который требуется для задачи\n");
    scanf("%d", &leftBorder);
    scanf("%d", &rightBorder);

    List *listWithNumbersLessThanLeftBorder = create();
    List *listWithNumbersInTheBorders = create();
    List *listWithNumbersGreaterThanRightBorder = create();

    int currentInputNumber = 0;
    FILE *inputFile = fopen("rewriteInput.txt", "r");
    while (fscanf(inputFile, "%d", &currentInputNumber) != EOF) {
        if (currentInputNumber)
    }

    return 0;
}