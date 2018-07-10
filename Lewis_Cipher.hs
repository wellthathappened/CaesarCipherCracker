—-Name: Ian Lewis
—-Date: 10/7/2017
—-Course: COP 4020
—-NID: ia474088
—-Assignment 4: Haskell Caesar Cipher

import Data.Char

ordCheck :: Char -> Bool
ordCheck a = if ((((ord a) > 64) && ((ord a) < 91)) || (((ord a) > 96) && ((ord a) < 123)))
             then True
             else False

let2nat :: Char -> Int
let2nat a = if (ord a) > 64 && (ord a) < 91
            then (ord a) - 65
            else (ord a) - 97

nat2let :: Int -> Char
nat2let = (!!) ['a'..'z']

shift :: Int -> Char -> Char
shift factor a = if (((isUpper a) == False) && ((ordCheck a) == True))
                 then nat2let ((let2nat a) + factor)
                 else a

encode :: Int -> String -> String
encode factor str = map (shift factor) str

decode :: Int -> String -> String
decode factor str = map (shift (-factor)) str

table :: [Float]
table = [8.2, 1.5, 2.8, 4.3, 12.7, 2.2, 2.0, 6.1, 7.0, 0.2, 0.8, 4.0, 2.4, 6.7, 7.5, 1.9, 0.1, 6.0, 6.3, 9.1, 2.8, 1.0, 2.4, 0.2, 2.0, 0.1]

lowers :: String -> Int
lowers a = length $ filter isLower a

count :: Char -> String -> Int
count a str = length $ filter (== a) str

percent :: Int -> Int -> Float
percent x y = ((fromIntegral x) / (fromIntegral y)) * 100

freqs :: String -> [Float]
freqs str = [percent (count a str) (lowers str) | a <- ['a'..'z']]

rotate :: Int -> [a] -> [a]
rotate placeNum str = (drop placeNum str) ++ (take placeNum str)

chisqr :: [Float] -> [Float] -> Float
chisqr os es = sum [(o - e)^2 / e | (o,e) <- zip os es]

position :: Eq a => a -> [a] -> Int
position dig list = head [elem | (found,elem) <- zip list [0..(length list - 1)], found == dig]

crack :: String -> String
crack str = decode factor str
            where
              frequencyList = freqs str
              chiSqList = [chisqr (rotate a frequencyList) table | a <- [0..25]]
              factor = position (minimum chiSqList) chiSqList