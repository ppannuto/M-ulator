#!/usr/bin/env python

import os
import sys
#from matplotlib import pyplot
from numpy import arange
import bisect
from decimal import *
import numpy
import csv


golden_mem_file = open("SRAM_4K.golden_mem", "w+")
count = 512
# mem_pattern1 = 'ffffffff'
# mem_pattern2 = '00000000'

mem_pattern1 = 'aaaaaaaa'
mem_pattern2 = '55555555'

for i in range (0, 512):
	golden_mem_file.write(mem_pattern1 + '\n')
	golden_mem_file.write(mem_pattern2 + '\n')
	
golden_mem_file.close()

