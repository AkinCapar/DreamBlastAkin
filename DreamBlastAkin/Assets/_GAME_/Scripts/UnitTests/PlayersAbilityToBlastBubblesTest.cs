using Zenject;
using System.Collections;
using System.Collections.Generic;
using DreamBlast.Controllers;
using DreamBlast.Installer;
using DreamBlast.Settings;
using DreamBlast.Utilities;
using DreamBlast.Views;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class PlayersAbilityToBlastBubblesTest : ZenjectIntegrationTestFixture
{
    private List<BubbleView> _remainingBubbles;
    private GameController _gameController;
    private BubblesController _bubblesController;

    [UnityTest]
    public IEnumerator BlueBubbleTest()
    {
        PreInstall();
        Bind();
        PostInstall();
        Initialize();

        yield return new WaitForSeconds(10f);

        BubbleBlastTest(BubbleColors.Blue);
    }

    [UnityTest]
    public IEnumerator GreenBubbleTest()
    {
        PreInstall();
        Bind();
        PostInstall();
        Initialize();

        yield return new WaitForSeconds(10f);

        BubbleBlastTest(BubbleColors.Green);
    }

    [UnityTest]
    public IEnumerator PurpleBubbleTest()
    {
        PreInstall();
        Bind();
        PostInstall();
        Initialize();

        yield return new WaitForSeconds(10f);

        BubbleBlastTest(BubbleColors.Purple);
    }

    [UnityTest]
    public IEnumerator PinkBubbleTest()
    {
        PreInstall();
        Bind();
        PostInstall();
        Initialize();

        yield return new WaitForSeconds(10f);

        BubbleBlastTest(BubbleColors.Pink);
    }

    [UnityTest]
    public IEnumerator RedBubbleTest()
    {
        PreInstall();
        Bind();
        PostInstall();
        Initialize();

        yield return new WaitForSeconds(10f);

        BubbleBlastTest(BubbleColors.Red);
    }


    [UnityTest]
    public IEnumerator YellowBubbleTest()
    {
        PreInstall();
        Bind();
        PostInstall();
        Initialize();

        yield return new WaitForSeconds(10f);

        BubbleBlastTest(BubbleColors.Yellow);
    }

    private void Bind()
    {
        BindSettings();
        GameSignalsInstaller.Install(Container);
        TestInstaller.Install(Container);
    }

    private void Initialize()
    {
        _gameController = Container.Resolve<GameController>();
        _gameController.Tick();
        _bubblesController = Container.Resolve<BubblesController>();
        _remainingBubbles = _bubblesController.GetRemainingBubbles();
    }

    private void BindSettings()
    {
        LevelSettings levelSettings =
            AssetDatabase.LoadAssetAtPath<LevelSettings>("Assets/Resources/TestSettings/LevelSettings 1.asset");
        Container.BindInterfacesAndSelfTo(levelSettings.GetType()).FromInstance(levelSettings);
        BubblesSettings bubblesSettings =
            AssetDatabase.LoadAssetAtPath<BubblesSettings>("Assets/Resources/TestSettings/BubblesSettings 1.asset");
        Container.BindInterfacesAndSelfTo(bubblesSettings.GetType()).FromInstance(bubblesSettings);
        PrefabSettings prefabSettings =
            AssetDatabase.LoadAssetAtPath<PrefabSettings>("Assets/Resources/TestSettings/PrefabSettings 1.asset");
        Container.BindInterfacesAndSelfTo(prefabSettings.GetType()).FromInstance(prefabSettings);
    }

    public void BubbleBlastTest(BubbleColors color)
    {
        int bubbleCountAtStart = _remainingBubbles.FindAll(bubble => bubble.GetBubbleColor() == color).Count;
        int bubbleCount = bubbleCountAtStart;

        if (bubbleCountAtStart < 1)
        {
            Debug.Log("There is no " + color + " bubbles in the game");
            Assert.Fail();
        }

        for (int i = 0; i < _remainingBubbles.Count; i++)
        {
            if (_remainingBubbles[i].GetBubbleColor() == color)
            {
                _bubblesController.CheckBubble(_remainingBubbles[i]);
            }

            bubbleCount = _remainingBubbles.FindAll(bubble => bubble.GetBubbleColor() == color).Count;

            if (bubbleCountAtStart > bubbleCount)
            {
                Assert.Pass();
                return;
            }
        }

        if (bubbleCountAtStart == bubbleCount)
        {
            Debug.Log(
                "There is " + color + " bubbles but there is no contacting blue bubbles more than 2. So there is no blastable " + color + " bubble.");
            Assert.Fail();
        }
    }
}