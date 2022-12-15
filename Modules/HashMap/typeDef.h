#pragma once

#include <stdlib.h>

typedef struct Type {
    char key[100];
    int value;
} Type;

int hashFunction(Type key);
