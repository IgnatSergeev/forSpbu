#include "graph.h"
#include "malloc.h"
#include "typeDef.h"
#include "queue.h"

struct Graph {
    int graphSize;
    NodeData *nodesDataArray;
    int **adjacencyMatrix;
};

Graph *createGraph(int numberOfNodes, NodeData nodesData[]) {
    Graph *graph = calloc(1, sizeof(Graph));
    if (graph == NULL) {
        return NULL;
    }
    graph->nodesDataArray = calloc(numberOfNodes, sizeof(NodeData));
    if (graph->nodesDataArray == NULL) {
        free(graph);
        return NULL;
    }
    for (int i = 0; i < numberOfNodes; i++) {
        graph->nodesDataArray[i] = nodesData[i];
    }
    graph->graphSize = numberOfNodes;
    graph->adjacencyMatrix = calloc(numberOfNodes, sizeof(int *));
    if (graph->adjacencyMatrix == NULL) {
        free(graph->nodesDataArray);
        free(graph);
        return NULL;
    }
    for (int i = 0; i < numberOfNodes; i++) {
        graph->adjacencyMatrix[i] = calloc(numberOfNodes, sizeof(int));
        for (int j = 0; j < numberOfNodes; j++) {
            if (i != j) {
                graph->adjacencyMatrix[i][j] = -1;
            }
        }
    }

    return graph;
}

void clearGraph(Graph *graph) {
    for (int j = 0; j < graph->graphSize; j++) {
        free(graph->adjacencyMatrix[j]);
    }
    free(graph->adjacencyMatrix);
    free(graph->nodesDataArray);
    free(graph);
}

void addEdge(Graph *graph, int indexOfStartNode, int indexOfEndNode, int length) {
    graph->adjacencyMatrix[indexOfStartNode][indexOfEndNode] = length;
    graph->adjacencyMatrix[indexOfEndNode][indexOfStartNode] = length;
}

void changeNodeData(Graph *graph, int index, NodeData nodeData) {
    graph->nodesDataArray[index] = nodeData;
}

void depthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int currentNodeIndex, bool *isVisited) {
    for (int i = 0; i < graph->graphSize; i++) {
        if (currentNodeIndex != i && graph->adjacencyMatrix[currentNodeIndex][i] != -1 && !isVisited[i]) {
            isVisited[i] = true;
            depthFirstSearch(graph, whatToDoWithTheValue, i, isVisited);
        }
    }
    graph->nodesDataArray[currentNodeIndex] = (*whatToDoWithTheValue)(graph->nodesDataArray[currentNodeIndex]);
}

void breadthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int currentNodeIndex, bool *isVisited) {
    for (int i = 0; i < graph->graphSize; i++) {
        if (currentNodeIndex != i && graph->adjacencyMatrix[currentNodeIndex][i] != -1 && !isVisited[i]) {
            isVisited[i] = true;
            depthFirstSearch(graph, whatToDoWithTheValue, i, isVisited);
        }
    }
    graph->nodesDataArray[currentNodeIndex] = (*whatToDoWithTheValue)(graph->nodesDataArray[currentNodeIndex]);
}

Node *findClosestToCapitalNode(NodesDataList *list, int countryIndex) {//даётся список со всеми крайними нодами непринадлежащими стране
    if (isEmpty(list)) {
        return NULL;
    }
    int minDistance = -1;
    Node *closestNode = NULL;
    Node *iteratorNode = list->head;
    while (iteratorNode != NULL) {
        if (minDistance == -1 || iteratorNode->value.distancesToTheCapitals[countryIndex] < minDistance) {
            minDistance = iteratorNode->value.distancesToTheCapitals[countryIndex];
            closestNode = iteratorNode;
        }
        iteratorNode = iteratorNode->next;
    }

    return closestNode;
}



int addNodeToTheCountry(Graph *graph, NodesDataList *list, int countryIndex) {//Dijkstra
    Node *pointerToTheClosestToCapitalNode = findClosestToCapitalNode(list, countryIndex);
    if (pointerToTheClosestToCapitalNode == NULL) {
        return -1;
    }
    Node closestToCapitalNode = *pointerToTheClosestToCapitalNode;
    deleteNode(list, closestToCapitalNode.value.index);
    graph->adjacencyLists[closestToCapitalNode.value.index]->nodeData.countryIndex = countryIndex;

    int startDistance = closestToCapitalNode.value.distancesToTheCapitals[countryIndex];
    AdjacencyList *listWithNeighbours = graph->adjacencyLists[closestToCapitalNode.value.index];
    Edge *iteratorNode = listWithNeighbours->head;
    while (iteratorNode != NULL) {
        int iteratorNodeIndex = iteratorNode->value.endNodeIndex;
        int distanceToCapital = iteratorNode->value.length + startDistance;
        NodeData currentNodeData = graph->adjacencyLists[iteratorNodeIndex]->nodeData;
        if (currentNodeData.countryIndex != countryIndex) {
            if (currentNodeData.countryIndex == -1) {
                if (currentNodeData.distancesToTheCapitals[countryIndex] == -1) {
                    graph->adjacencyLists[iteratorNodeIndex]->nodeData.distancesToTheCapitals[countryIndex] = distanceToCapital;
                    insertNode(list, currentNodeData, 0);
                } else {
                    if (currentNodeData.distancesToTheCapitals[countryIndex] > distanceToCapital) {
                        graph->adjacencyLists[iteratorNodeIndex]->nodeData.distancesToTheCapitals[countryIndex] = distanceToCapital;
                    }
                }
            }
        }
        iteratorNode = iteratorNode->next;
    }

    return 0;
}