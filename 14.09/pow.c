#include <stdio.h>
#include <stdbool.h>
#include <math.h>

double linearPow(int base, int degree) {
    if ((base == 0) && (degree < 0)) {
        return 0;
    }
    if (degree < 0) {
        return 1 / linearPow(base, -degree);
    }

    int result = 1;
    for (int i = 0; i < degree; i++) {
        result *= base;
    }

    return result;
}

double logarithmicPow(int base, int degree) {
    if ((base == 0) && (degree < 0)) {
        return 0;
    }

    if (degree < 0) {
        return 1 / linearPow(base, -degree);
    }

    if (degree == 0) {
        return 1;
    }

    if (degree % 2 == 0) {
        double prevIteration = logarithmicPow(base, degree / 2);

        return prevIteration * prevIteration;
    }
    return logarithmicPow(base, degree - 1) * base;
}

bool tester() {
    bool typicalTest = (linearPow(2, 3) == 8) && (linearPow(2, 2) == 4) && (logarithmicPow(2, 2) == 4) &&
            (logarithmicPow(2, 3) == 8) && (linearPow(2, -2) == 0.25) && (logarithmicPow(2, -2) == 0.25);
    bool incorrectInputTest = (linearPow(0, -5) == 0) && (logarithmicPow(0, -5) == 0);

    return typicalTest && incorrectInputTest;
}

int main() {
    if (!tester()) {
        printf("Tests failed");

        return 0;
    } else {
        printf("Tests passed\n");
    }

    int inputBase = 0;
    int inputDegree = 0;

    printf("%s", "Enter the base and then the degree to which you want to pow\n");
    scanf("%d", &inputBase);
    scanf("%d", &inputDegree);

    printf("Here is the result of the linear pow:\n%lf\n", linearPow(inputBase, inputDegree));
    printf("Here is the result of the logarithmic pow:\n%lf\n", logarithmicPow(inputBase, inputDegree));

    return 0;
}
