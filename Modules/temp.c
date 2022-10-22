#include <stdio.h>
#include "customStack.h"
#include <malloc.h>

int main() {
    Stack* stack = createStack();
    push(stack, 100);
    printf("%d", top(stack));

    free(stack);
    return 0;
}