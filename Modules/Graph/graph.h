#pragma once

#include "typeDef.h"
#include <stdbool.h>
#include "nodesDataList.h"

typedef struct Graph Graph;

Graph *createGraph(int numberOfNodes, NodeData nodesData[]);

void clearGraph(Graph *graph);

int addEdge(Graph *graph, int indexOfStartNode, EdgeProperties edgeProperties);

NodeData getNodeData(Graph *graph, int index);

void changeNodeData(Graph *graph, int index, NodeData nodeData);

void depthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int currentNodeIndex);

int addNodeToTheCountry(Graph *graph, NodesDataList *list, int countryIndex);

void assignGraphsNumberOfCapitals(Graph *graph, int numberOfCapitals);

int **print(Graph *graph, bool isTest);