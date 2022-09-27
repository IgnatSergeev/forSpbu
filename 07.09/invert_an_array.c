#include <stdio.h>

void reverseTheArrayPartWithBothEndsIncluded(int array[1001], int reversingStartInd, int reversingEndInd) {
    int reversingPartLength = reversingEndInd - reversingStartInd + 1;
    int centerElementInPartToReverse = (reversingPartLength / 2) - 1;

    for (int i = 0; i <= centerElementInPartToReverse; i++) {
        int variableToEasilyChangeTwoInts = array[reversingStartInd + i];
        array[reversingStartInd + i] = array[reversingEndInd - i];
        array[reversingEndInd - i] = variableToEasilyChangeTwoInts;
    }
}

int main() {
    int array[1001] = {0};
    int firstPartSize = 0;
    int secondPartSize = 0;

    printf("%s", "Введите количество элементов частей массива(сумма длин этих частей не должна превышать 1000), которые нужно поменять местами, а затем элементы массива через пробел(их количество не должно превышать 1000 элементов)\n");
    scanf("%d", &firstPartSize);
    scanf("%d", &secondPartSize);
    int arraySize = firstPartSize + secondPartSize;

    for (int i = 0; i < arraySize; i++) {
        int arrayElement = 0;
        scanf("%d", &arrayElement);
        array[i] = arrayElement;
    }

    reverseTheArrayPartWithBothEndsIncluded(array, 0, firstPartSize - 1);
    reverseTheArrayPartWithBothEndsIncluded(array, firstPartSize,  arraySize - 1);
    reverseTheArrayPartWithBothEndsIncluded(array, 0,  arraySize - 1);

    printf("%s", "Вот массив после переставления первой и второй частей: ");
    for (int i = 0; i < arraySize; i++) {
        printf("%d ", array[i]);
    }

    return 0;
}
