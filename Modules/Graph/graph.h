#pragma once

#include "typeDef.h"

typedef struct Graph Graph;

typedef struct NodesDataList NodesDataList;

Graph *createGraph(int numberOfNodes, NodeData nodesData[]);

void clearGraph(Graph *graph);

int addEdge(Graph *graph, int indexOfStartNode, EdgeProperties edgeProperties);

void depthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int currentNodeIndex);

int addNodeToTheCountry(Graph *graph, NodesDataList *list, int countryIndex);