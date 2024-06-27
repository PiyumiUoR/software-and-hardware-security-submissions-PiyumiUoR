## Task 0

The function was tested with multiple assert statements and the results were as below. 

The input was provided to prove that the square root of 4 is 2. This command provides 2 as the result proving that the condition is true. 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/216e7ce9-53fa-4439-ad16-b379d1c586f1)

If the statement is checked with the false inputs, the result will be as shown in the below screenshot. 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/b32d7a03-b770-483f-ab88-4ef01bdd78ef)

The iterations can be observed if the function is called with the desirable input. 

```my_sqrt(3)```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/3410d439-5405-4ab4-8b17-22a6ae69a62d)

```my_sqrt(168)```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/3fbab41e-8fea-4545-b8a3-176f9948bcd0)

## Task 1

### Task 1.A

Printing 10 malformed samples of ```Fuzztest 1337``` using echo.

Command line: ```echo "Fuzztest 1337" | radamsa -n 10``` & ```echo "Fuzztest 1337" | radamsa --seed 3 -n 10```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/ee269272-6272-42d2-9d57-dd9d087e0c6d)

### Task 1.B

1. Creating the .txt file that contains only the text 12 EF. 

```echo 12 EF > test.txt```

2. Using Radamsa to generate 100 fuzzed samples of the file that are stored in a single file called fuzz.txt and creating a separate folder for the samples.

```mkdir Samples```

```
for i in {1..100}; do
cat test.txt | radamsa > Samples/$i.txt
cat Samples/$i.txt >> fuzz.txt
done
```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/90e3c6a5-2d10-496e-b345-8bf5293fc234)

The screenshot of the ```Samples``` folder looks like as shown below.

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/8e5fcf70-6878-40e3-9380-5f21a508a59f)

The samples:

1. ```1.txt```.

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/d500806a-9ffc-4493-b97f-d17206be3b4a)

2. ```20.txt```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/7d5926b8-aa69-459c-892f-d9f8dfc2333e)
 
3. ```46.txt```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/1401889d-16d0-4cc4-a08f-6d2d7115795b)

4. ```61.txt```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/0965cfb6-f6d4-49f7-9c83-2ade9a00e4b4)

5. 92.txt

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/b60b69e3-ef69-4b65-aae9-05d3e74a8c08)


## Task 2

There is an error showing up. Couldn't figure out the error. 
```
/Documents/CS_III_EX_1/task2/B/unrtf-0.21.5
❯ ./configure CC="</usr/bin/afl-gcc-fast>" --prefix=$home/unrtf
checking for a BSD-compatible install... /usr/bin/install -c
checking whether build environment is sane... yes
checking for a thread-safe mkdir -p... /usr/bin/mkdir -p
checking for gawk... gawk
checking whether make sets $(MAKE)... yes
checking whether make supports nested variables... yes
checking whether to enable maintainer-specific portions of Makefiles... no
checking for gcc... </usr/bin/afl-gcc-fast>
checking whether the C compiler works... no
configure: error: in `/home/arch/Documents/CS_III_EX_1/task2/B/unrtf-0.21.5':
configure: error: C compiler cannot create executables
See `config.log' for more details
```

...

## Task 3

There is an error showing up. Couldn't figure out the error.

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/38bb8043-6e8a-4b42-a517-ffb8586b8597)
...


## Task 4

### Task 4.A

The mutation was created using Python. 
Reference: https://www.fuzzingbook.org/html/MutationFuzzer.html

The code to run the mutator for a specific time period:

```
import random
import time

def delete_random_character(s: str) -> str:
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    return s[:pos] + s[pos + 1:]

def insert_random_character(s: str) -> str:
    
    pos = random.randint(0, len(s))
    random_character = chr(random.randrange(32, 127))
    return s[:pos] + random_character + s[pos:]

def flip_random_character(s):
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    c = s[pos]
    bit = 1 << random.randint(0, 6)
    new_c = chr(ord(c) ^ bit)
    return s[:pos] + new_c + s[pos + 1:]

def mutate(s: str) -> str:
    
    mutators = [
        delete_random_character,
        insert_random_character,
        flip_random_character
    ]
    mutator = random.choice(mutators)
    
    return mutator(s)

seed_input = "http://www.google.com/search?q=fuzzing"
# mutations = 5
inp = seed_input

### Running the program for a specific time period

duration = 0.1
start_at = time.time()
i = 0
while True:
    if time.time() - start_at <= duration:
            i += 1
            print(i, mutate(inp))
            inp = seed_input
            
    if time.time() - start_at >= duration:
        break 
```

The screenshot shows the number of mutations the program provided for 0.1 seconds. The total number of mutations is 277/0.1 second. This number of mutation is not a constant for each time the program is executed. 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/4acfab7b-dd03-4d82-9793-9f2d79a18972)
![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/ed22777b-72cb-423c-8ada-768ca3f2005c)

The code to run the mutator for a specific mutation count:

```
import random
import time

def delete_random_character(s: str) -> str:
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    return s[:pos] + s[pos + 1:]

def insert_random_character(s: str) -> str:
    
    pos = random.randint(0, len(s))
    random_character = chr(random.randrange(32, 127))
    return s[:pos] + random_character + s[pos:]

def flip_random_character(s):
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    c = s[pos]
    bit = 1 << random.randint(0, 6)
    new_c = chr(ord(c) ^ bit)
    return s[:pos] + new_c + s[pos + 1:]

def mutate(s: str) -> str:
    
    mutators = [
        delete_random_character,
        insert_random_character,
        flip_random_character
    ]
    mutator = random.choice(mutators)
    
    return mutator(s)

seed_input = "http://www.google.com/search?q=fuzzing"
mutations = 20
inp = seed_input
  
### Running the program for a specific mutation count
for i in range(mutations):
    if i <= mutations:
        print(i, "mutations:", mutate(inp))
        inp = seed_input
```

The mutations are: 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/3b171bc4-6da5-4ce2-a2de-832452bf44a8)

The sample ```.ipynb``` is in the misc folder. 

## Task 4.B

The validator was created using the ```re``` module in Python. The code is as follows. 

```
import re

def validate_url(url):
    pattern = r'^(http|https|ftp)://([\w.-]+)(/[\w./]*)?(\?[\w=&-]+)?(#\w+)?$'
    if re.match(pattern, url):
        return True
    else:
        return False


url = input("Input the URL: ")
print(f"The URL is {validate_url(url)}")
```

The screenshot shows that if a URL is provided as an input for the program, the output is generated successfully. 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/e1daec93-8090-4d7a-8cb1-fd0d3f440ee5)

The generated mutations in task 4.A is provided as inputs for the program.

```
import re

def validate_url(url):
    pattern = r'^(http|https|ftp)://([\w.-]+)(/[\w./]*)?(\?[\w=&-]+)?(#\w+)?$'
    if re.match(pattern, url):
        return True
    else:
        return False
    
mutations1 = 'http://www.google.com/search?qfuzzing'
mutations2 = 'http://www.google.coe/search?q=fuzzing'
mutations3 = 'http://www.google.com/search?q=fuzzi.g'
mutations4 = 'http://www.google.Com/search?q=fuzzing'
mutations5 = 'http://www.googe.com/search?q=fuzzing'
mutations6 = 'http://www.google.com/searchq=fuzzing'
mutations7 = 'http://www.google.com/separch?q=fuzzing'
mutations8 = 'http:U//www.google.com/search?q=fuzzing'
mutations9 = 'http://www.google.com/search?q=Gfuzzing'
mutations10 = 'htp://www.google.com/search?q=fuzzing'
mutations11 = 'http://www.google.com/search?q=fuazzing'
mutations12 = 'http://wwbw.google.com/search?q=fuzzing'
mutations13 = 'http://www.google.com/search/q=fuzzing'
mutations14 = 'http://www.gooMgle.com/search?q=fuzzing'
mutations15 = 'http://www.googne.com/search?q=fuzzing'
mutations16 = 'http://www.google.com/search?p=fuzzing'
mutations17 = 'http://www.google.com/se=arch?q=fuzzing'
mutations18 = 'http://www.google.com/search?vq=fuzzing'
mutations19 = 'http://www.gogle.com/search?q=fuzzing'
mutations20 = 'http://www.google.com/s{earch?q=fuzzing'

print(f"The URL is {validate_url(mutations1)}")
print(f"The URL is {validate_url(mutations2)}")
print(f"The URL is {validate_url(mutations3)}")
print(f"The URL is {validate_url(mutations4)}")
print(f"The URL is {validate_url(mutations5)}")
print(f"The URL is {validate_url(mutations6)}")
print(f"The URL is {validate_url(mutations7)}")
print(f"The URL is {validate_url(mutations8)}")
print(f"The URL is {validate_url(mutations9)}")
print(f"The URL is {validate_url(mutations10)}")
print(f"The URL is {validate_url(mutations11)}")
print(f"The URL is {validate_url(mutations12)}")
print(f"The URL is {validate_url(mutations13)}")
print(f"The URL is {validate_url(mutations14)}")
print(f"The URL is {validate_url(mutations15)}")
print(f"The URL is {validate_url(mutations16)}")
print(f"The URL is {validate_url(mutations17)}")
print(f"The URL is {validate_url(mutations18)}")
print(f"The URL is {validate_url(mutations19)}")
print(f"The URL is {validate_url(mutations20)}")
```

The output:

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/02baa7e4-74c0-4a2f-ba6c-0b3dbeb7f7ca)

The program did not encounter any errors for the given inputs. 

## Task 4.C

A fuzzer program was made for the URL testing. 

The code: 
```
import random
import time
import re

def delete_random_character(s: str) -> str:
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    return s[:pos] + s[pos + 1:]

def insert_random_character(s: str) -> str:
    
    pos = random.randint(0, len(s))
    random_character = chr(random.randrange(32, 127))
    return s[:pos] + random_character + s[pos:]

def flip_random_character(s):
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    c = s[pos]
    bit = 1 << random.randint(0, 6)
    new_c = chr(ord(c) ^ bit)
    return s[:pos] + new_c + s[pos + 1:]

def mutate(s: str) -> str:
    
    mutators = [
        delete_random_character,
        insert_random_character,
        flip_random_character
    ]
    mutator = random.choice(mutators)
    
    return mutator(s)

def validate_url(url):
    pattern = r'^(http|https|ftp)://([\w.-]+)(/[\w./]*)?(\?[\w=&-]+)?(#\w+)?$'
    if re.match(pattern, url):
        return True
    else:
        return False

def urlFuzzer(noOfTests):
    
    seed_input = "http://www.google.com/search?q=fuzzing"
    # mutations = 20
    inp = seed_input
    crashes = 0

    for i in range(noOfTests):
        if i <= noOfTests:
            inp = mutate(inp)
            # print("Test case ", (i+1), ": ", repr(inp))
            
            if not validate_url(inp):
                crashes += 1
                print("A crash occured in " + str(i+1))
                print("The input is: ", inp)
            inp = seed_input
    
    print("The total crash count is: ", crashes)

if __name__ == "__main__":
    noOfTests = int(input("Number of test cases? "))
    urlFuzzer(noOfTests)
```

The results for 100 test cases:

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/15ce164a-d27b-46a0-b8ad-c4d8f97047c8)

The results for 1000 test cases:

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/54eaae65-748c-4bf6-bbbf-f447c34076e7)

The results for 10,000 test cases:

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/578f9761-8870-4f3f-9f6c-fe08d4ff5b98)

## Task 4.D

The radamsa output files are attached in the misc folder. 
## Task 0

The function was tested with multiple assert statements and the results were as below. 

The input was provided to prove that the square root of 4 is 2. This command provides 2 as the result proving that the condition is true. 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/216e7ce9-53fa-4439-ad16-b379d1c586f1)

If the statement is checked with the false inputs, the result will be as shown in the below screenshot. 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/b32d7a03-b770-483f-ab88-4ef01bdd78ef)

The iterations can be observed if the function is called with the desirable input. 

```my_sqrt(3)```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/3410d439-5405-4ab4-8b17-22a6ae69a62d)

```my_sqrt(168)```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/3fbab41e-8fea-4545-b8a3-176f9948bcd0)

## Task 1

### Task 1.A

Printing 10 malformed samples of ```Fuzztest 1337``` using echo.

Command line: ```echo "Fuzztest 1337" | radamsa -n 10``` & ```echo "Fuzztest 1337" | radamsa --seed 3 -n 10```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/ee269272-6272-42d2-9d57-dd9d087e0c6d)

### Task 1.B

1. Creating the .txt file that contains only the text 12 EF. 

```echo 12 EF > test.txt```

2. Using Radamsa to generate 100 fuzzed samples of the file that are stored in a single file called fuzz.txt and creating a separate folder for the samples.

```mkdir Samples```

```
for i in {1..100}; do
cat test.txt | radamsa > Samples/$i.txt
cat Samples/$i.txt >> fuzz.txt
done
```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/90e3c6a5-2d10-496e-b345-8bf5293fc234)

The screenshot of the ```Samples``` folder looks like as shown below.

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/8e5fcf70-6878-40e3-9380-5f21a508a59f)

The samples:

1. ```1.txt```.

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/d500806a-9ffc-4493-b97f-d17206be3b4a)

2. ```20.txt```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/7d5926b8-aa69-459c-892f-d9f8dfc2333e)
 
3. ```46.txt```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/1401889d-16d0-4cc4-a08f-6d2d7115795b)

4. ```61.txt```

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/0965cfb6-f6d4-49f7-9c83-2ade9a00e4b4)

5. 92.txt

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/b60b69e3-ef69-4b65-aae9-05d3e74a8c08)


## Task 2

There is an error showing up. Couldn't figure out the error. 
```
/Documents/CS_III_EX_1/task2/B/unrtf-0.21.5
❯ ./configure CC="</usr/bin/afl-gcc-fast>" --prefix=$home/unrtf
checking for a BSD-compatible install... /usr/bin/install -c
checking whether build environment is sane... yes
checking for a thread-safe mkdir -p... /usr/bin/mkdir -p
checking for gawk... gawk
checking whether make sets $(MAKE)... yes
checking whether make supports nested variables... yes
checking whether to enable maintainer-specific portions of Makefiles... no
checking for gcc... </usr/bin/afl-gcc-fast>
checking whether the C compiler works... no
configure: error: in `/home/arch/Documents/CS_III_EX_1/task2/B/unrtf-0.21.5':
configure: error: C compiler cannot create executables
See `config.log' for more details
```

...

## Task 3

There is an error showing up. Couldn't figure out the error.

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/38bb8043-6e8a-4b42-a517-ffb8586b8597)
...


## Task 4

### Task 4.A

The mutation was created using Python. 
Reference: https://www.fuzzingbook.org/html/MutationFuzzer.html

The code to run the mutator for a specific time period:

```
import random
import time

def delete_random_character(s: str) -> str:
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    return s[:pos] + s[pos + 1:]

def insert_random_character(s: str) -> str:
    
    pos = random.randint(0, len(s))
    random_character = chr(random.randrange(32, 127))
    return s[:pos] + random_character + s[pos:]

def flip_random_character(s):
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    c = s[pos]
    bit = 1 << random.randint(0, 6)
    new_c = chr(ord(c) ^ bit)
    return s[:pos] + new_c + s[pos + 1:]

def mutate(s: str) -> str:
    
    mutators = [
        delete_random_character,
        insert_random_character,
        flip_random_character
    ]
    mutator = random.choice(mutators)
    
    return mutator(s)

seed_input = "http://www.google.com/search?q=fuzzing"
# mutations = 5
inp = seed_input

### Running the program for a specific time period

duration = 0.1
start_at = time.time()
i = 0
while True:
    if time.time() - start_at <= duration:
            i += 1
            print(i, mutate(inp))
            inp = seed_input
            
    if time.time() - start_at >= duration:
        break 
```

The screenshot shows the number of mutations the program provided for 0.1 seconds. The total number of mutations is 277/0.1 second. This number of mutation is not a constant for each time the program is executed. 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/4acfab7b-dd03-4d82-9793-9f2d79a18972)
![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/ed22777b-72cb-423c-8ada-768ca3f2005c)

The code to run the mutator for a specific mutation count:

```
import random
import time

def delete_random_character(s: str) -> str:
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    return s[:pos] + s[pos + 1:]

def insert_random_character(s: str) -> str:
    
    pos = random.randint(0, len(s))
    random_character = chr(random.randrange(32, 127))
    return s[:pos] + random_character + s[pos:]

def flip_random_character(s):
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    c = s[pos]
    bit = 1 << random.randint(0, 6)
    new_c = chr(ord(c) ^ bit)
    return s[:pos] + new_c + s[pos + 1:]

def mutate(s: str) -> str:
    
    mutators = [
        delete_random_character,
        insert_random_character,
        flip_random_character
    ]
    mutator = random.choice(mutators)
    
    return mutator(s)

seed_input = "http://www.google.com/search?q=fuzzing"
mutations = 20
inp = seed_input
  
### Running the program for a specific mutation count
for i in range(mutations):
    if i <= mutations:
        print(i, "mutations:", mutate(inp))
        inp = seed_input
```

The mutations are: 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/3b171bc4-6da5-4ce2-a2de-832452bf44a8)

The sample ```.ipynb``` is in the misc folder. 

## Task 4.B

The validator was created using the ```re``` module in Python. The code is as follows. 

```
import re

def validate_url(url):
    pattern = r'^(http|https|ftp)://([\w.-]+)(/[\w./]*)?(\?[\w=&-]+)?(#\w+)?$'
    if re.match(pattern, url):
        return True
    else:
        return False


url = input("Input the URL: ")
print(f"The URL is {validate_url(url)}")
```

The screenshot shows that if a URL is provided as an input for the program, the output is generated successfully. 

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/e1daec93-8090-4d7a-8cb1-fd0d3f440ee5)

The generated mutations in task 4.A is provided as inputs for the program.

```
import re

def validate_url(url):
    pattern = r'^(http|https|ftp)://([\w.-]+)(/[\w./]*)?(\?[\w=&-]+)?(#\w+)?$'
    if re.match(pattern, url):
        return True
    else:
        return False
    
mutations1 = 'http://www.google.com/search?qfuzzing'
mutations2 = 'http://www.google.coe/search?q=fuzzing'
mutations3 = 'http://www.google.com/search?q=fuzzi.g'
mutations4 = 'http://www.google.Com/search?q=fuzzing'
mutations5 = 'http://www.googe.com/search?q=fuzzing'
mutations6 = 'http://www.google.com/searchq=fuzzing'
mutations7 = 'http://www.google.com/separch?q=fuzzing'
mutations8 = 'http:U//www.google.com/search?q=fuzzing'
mutations9 = 'http://www.google.com/search?q=Gfuzzing'
mutations10 = 'htp://www.google.com/search?q=fuzzing'
mutations11 = 'http://www.google.com/search?q=fuazzing'
mutations12 = 'http://wwbw.google.com/search?q=fuzzing'
mutations13 = 'http://www.google.com/search/q=fuzzing'
mutations14 = 'http://www.gooMgle.com/search?q=fuzzing'
mutations15 = 'http://www.googne.com/search?q=fuzzing'
mutations16 = 'http://www.google.com/search?p=fuzzing'
mutations17 = 'http://www.google.com/se=arch?q=fuzzing'
mutations18 = 'http://www.google.com/search?vq=fuzzing'
mutations19 = 'http://www.gogle.com/search?q=fuzzing'
mutations20 = 'http://www.google.com/s{earch?q=fuzzing'

print(f"The URL is {validate_url(mutations1)}")
print(f"The URL is {validate_url(mutations2)}")
print(f"The URL is {validate_url(mutations3)}")
print(f"The URL is {validate_url(mutations4)}")
print(f"The URL is {validate_url(mutations5)}")
print(f"The URL is {validate_url(mutations6)}")
print(f"The URL is {validate_url(mutations7)}")
print(f"The URL is {validate_url(mutations8)}")
print(f"The URL is {validate_url(mutations9)}")
print(f"The URL is {validate_url(mutations10)}")
print(f"The URL is {validate_url(mutations11)}")
print(f"The URL is {validate_url(mutations12)}")
print(f"The URL is {validate_url(mutations13)}")
print(f"The URL is {validate_url(mutations14)}")
print(f"The URL is {validate_url(mutations15)}")
print(f"The URL is {validate_url(mutations16)}")
print(f"The URL is {validate_url(mutations17)}")
print(f"The URL is {validate_url(mutations18)}")
print(f"The URL is {validate_url(mutations19)}")
print(f"The URL is {validate_url(mutations20)}")
```

The output:

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/02baa7e4-74c0-4a2f-ba6c-0b3dbeb7f7ca)

The program did not encounter any errors for the given inputs. 

## Task 4.C

A fuzzer program was made for the URL testing. 

The code: 
```
import random
import time
import re

def delete_random_character(s: str) -> str:
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    return s[:pos] + s[pos + 1:]

def insert_random_character(s: str) -> str:
    
    pos = random.randint(0, len(s))
    random_character = chr(random.randrange(32, 127))
    return s[:pos] + random_character + s[pos:]

def flip_random_character(s):
    
    if s == "":
        return s

    pos = random.randint(0, len(s) - 1)
    c = s[pos]
    bit = 1 << random.randint(0, 6)
    new_c = chr(ord(c) ^ bit)
    return s[:pos] + new_c + s[pos + 1:]

def mutate(s: str) -> str:
    
    mutators = [
        delete_random_character,
        insert_random_character,
        flip_random_character
    ]
    mutator = random.choice(mutators)
    
    return mutator(s)

def validate_url(url):
    pattern = r'^(http|https|ftp)://([\w.-]+)(/[\w./]*)?(\?[\w=&-]+)?(#\w+)?$'
    if re.match(pattern, url):
        return True
    else:
        return False

def urlFuzzer(noOfTests):
    
    seed_input = "http://www.google.com/search?q=fuzzing"
    # mutations = 20
    inp = seed_input
    crashes = 0

    for i in range(noOfTests):
        if i <= noOfTests:
            inp = mutate(inp)
            # print("Test case ", (i+1), ": ", repr(inp))
            
            if not validate_url(inp):
                crashes += 1
                print("A crash occured in " + str(i+1))
                print("The input is: ", inp)
            inp = seed_input
    
    print("The total crash count is: ", crashes)

if __name__ == "__main__":
    noOfTests = int(input("Number of test cases? "))
    urlFuzzer(noOfTests)
```

The results for 100 test cases:

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/15ce164a-d27b-46a0-b8ad-c4d8f97047c8)

The results for 1000 test cases:

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/54eaae65-748c-4bf6-bbbf-f447c34076e7)

The results for 10,000 test cases:

![image](https://github.com/SecurityEngineering-2023/security-engineering-submissions-PiyumiUoR/assets/53691448/578f9761-8870-4f3f-9f6c-fe08d4ff5b98)

## Task 4.D

The radamsa output files are attached in the misc folder. 
