#pragma once

#include <stdbool.h>
#include "typeDef.h"

typedef struct HashMap HashMap;

HashMap *createHashMap();

int addValue(HashMap *hashMap, Type key, int keySize);

void deleteValue(HashMap *hashMap, Type key);

void clearHashMap(HashMap *hashMap);