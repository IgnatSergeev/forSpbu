#include <stdio.h>
#include "customStack.h"

int main() {
    Stack* stack = createStack();
    push(stack, 100);
    printf("%d", top(stack));
}