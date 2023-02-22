#pragma once

#include "typeDef.h"

typedef struct NodesDataList NodesDataList;

NodesDataList *create();

void clear(NodesDataList *list);

int insertNode(NodesDataList *list, NodeData value, int index);

int insertNodeToEnd(NodesDataList *list, NodeData value);