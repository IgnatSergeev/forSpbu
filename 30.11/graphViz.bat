@echo off

start dot -Tpng graphViz.dot -o graphViz.png
timeout 1
start graphViz.png