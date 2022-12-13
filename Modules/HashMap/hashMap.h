#pragma once

#include <stdbool.h>
#include "typeDef.h"

typedef struct HashMap HashMap;

HashMap *createHashMap();

int addValue(HashMap *hashMap, Type key, int keySize, Type (*whatToDoIfAlreadyExist)(Type));

void deleteValue(HashMap *hashMap, Type key);

void clearHashMap(HashMap *hashMap);