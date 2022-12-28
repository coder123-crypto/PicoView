// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace PicoView.Core.Dispatchers;

public class Dispatcher
{
    public static void Init(IMessagesDispatcher messagesDispatcher, IDialogsDispatcher dialogsDispatcher, IViewsDispatcher viewsDispatcher, ICommandsDispatcher commandsDispatcher)
    {
        _dispatcher = new Dispatcher
        {
            MessagesDispatcher = messagesDispatcher,
            DialogsDispatcher = dialogsDispatcher,
            ViewsDispatcher = viewsDispatcher,
            CommandsDispatcher = commandsDispatcher
        };
    }
    
    public static Dispatcher Pool
    {
        get
        {
            if (_dispatcher == null)
            {
                throw new NullReferenceException("Pool must be initialized");
            }

            return _dispatcher;
        }
    }
    
    public IMessagesDispatcher MessagesDispatcher { get; private init; }
    
    public IDialogsDispatcher DialogsDispatcher { get; private init; }
    
    public IViewsDispatcher ViewsDispatcher { get; private init; }
    
    public ICommandsDispatcher CommandsDispatcher { get; private init; }
    
    private Dispatcher()
    {
    }
    
    private static Dispatcher _dispatcher;
}