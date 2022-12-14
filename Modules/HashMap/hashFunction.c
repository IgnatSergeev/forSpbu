#pragma once

#include "typeDef.h"

#define p 13
#define SQRT_INT_MAX 46340

//hashFunction should return number >= 0 and < HASH_FUNCTION_RANGE
int hashFunction(Type element) {
    char *key = element.string;
    int index = 0;
    int currentP = 1;
    int result = 0;
    while (key[index] != '\0') {
        result = (result + (currentP * key[index]) % (SQRT_INT_MAX)) % SQRT_INT_MAX;
        currentP = (currentP * p) % SQRT_INT_MAX;
        ++index;
    }

    return result;
}