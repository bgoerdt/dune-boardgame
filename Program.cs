﻿// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Arrakis!");

var gameEngine = new GameEngine();

gameEngine.Play();



/*
TODO
    -- character class X
    -- equipment card class X
    -- kanly card class X
    -- player class x
    -- board space class x
    -- game board x
    -- game engine
        -- setup X
        -- move
            -- get possible moves x
            -- implement move actions
                -- spice raid X
                -- poison X
                -- space guild X
                -- traitor 
                -- bene gesserit 
            -- choose move (random for now) x
            -- determine if another move is possible x
        -- fight
        -- buy
            -- implement card deck
        -- invest

    -- future work
        -- implement finite amount of spice/harvesters

    -- architecture ideas
        -- giant state machine, like redux?

*/
