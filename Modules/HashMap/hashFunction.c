#pragma once

#include "typeDef.h"
#define p 13
//hashFunction should return number >= 0 and < HASH_FUNCTION_RANGE
int hashFunction(Type key) {
    int index = 0;
    int currentP = 1;
    int result = 0;
    while (key[index] != '\0') {
        result = (result + (currentP * key[index]) % HASH_FUNCTION_RANGE) % HASH_FUNCTION_RANGE;
        currentP = (currentP * p) % HASH_FUNCTION_RANGE;
        ++index;
    }

    return result;
}