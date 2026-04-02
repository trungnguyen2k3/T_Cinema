import React, {
  useCallback,
  useEffect,
  useMemo,
  useRef,
  useState,
} from "react";
import * as signalR from "@microsoft/signalr";
import "./ChatComponent.css";
import { CHAT_CONFIG } from "../../configs/chatConfig";

export default function ChatComponent() {
  const { API_BASE, DEFAULT_CURRENT_USER } = CHAT_CONFIG;

  const [users, setUsers] = useState([]);
  const [currentUser, setCurrentUser] = useState(DEFAULT_CURRENT_USER);
  const [chatWith, setChatWith] = useState(0);
  const [messages, setMessages] = useState([]);
  const [messageInput, setMessageInput] = useState("");
  const [loadingUsers, setLoadingUsers] = useState(false);
  const [isConnected, setIsConnected] = useState(false);

  const connectionRef = useRef(null);
  const chatBoxRef = useRef(null);

  const formatTime = useCallback((dateStr) => {
    if (!dateStr) return "";
    const d = new Date(dateStr);
    if (Number.isNaN(d.getTime())) return dateStr;
    return d.toLocaleTimeString("vi-VN", {
      hour: "2-digit",
      minute: "2-digit",
    });
  }, []);

//   const formatLastSeen = useCallback((dateStr) => {
//     if (!dateStr) return "";
//     const d = new Date(dateStr);
//     if (Number.isNaN(d.getTime())) return "";
//     return d.toLocaleString("vi-VN");
//   }, []);

  const clearChat = useCallback(() => {
    setMessages([]);
  }, []);

  const scrollChatToBottom = useCallback(() => {
    if (chatBoxRef.current) {
      chatBoxRef.current.scrollTop = chatBoxRef.current.scrollHeight;
    }
  }, []);

  const chatUsers = useMemo(() => {
    return users.filter((u) => u.id !== Number(currentUser));
  }, [users, currentUser]);

//   const currentUserInfo = useMemo(() => {
//     return users.find((u) => u.id === Number(currentUser)) || null;
//   }, [users, currentUser]);

  const chatWithUser = useMemo(() => {
    return users.find((u) => u.id === Number(chatWith)) || null;
  }, [users, chatWith]);

  const isCurrentConversationMessage = useCallback(
    (msg) => {
      const me = Number(currentUser);
      const other = Number(chatWith);

      return (
        (msg.senderId === me && msg.receiverId === other) ||
        (msg.senderId === other && msg.receiverId === me)
      );
    },
    [currentUser, chatWith],
  );

  const appendMessage = useCallback(
    (msg) => {
      if (!isCurrentConversationMessage(msg)) return;
      setMessages((prev) => [...prev, msg]);
    },
    [isCurrentConversationMessage],
  );

  const loadUsers = useCallback(async () => {
    try {
      setLoadingUsers(true);

      const res = await fetch(`${API_BASE}/api/account/all`);
      const json = await res.json();

      const accountList = json.data ?? [];

      if (!Array.isArray(accountList) || accountList.length === 0) {
        setUsers([]);
        return;
      }

      const activeUsers = accountList.filter((x) => x.status !== false);
      setUsers(activeUsers);

      const defaultCurrent =
        DEFAULT_CURRENT_USER &&
        activeUsers.some((x) => x.id === DEFAULT_CURRENT_USER)
          ? DEFAULT_CURRENT_USER
          : activeUsers[0]?.id || 0;

      const defaultChatWith =
        activeUsers.find((x) => x.id !== defaultCurrent)?.id || 0;

      setCurrentUser(defaultCurrent);
      setChatWith(defaultChatWith);
    } catch (err) {
      console.error("Load users lỗi:", err);
    } finally {
      setLoadingUsers(false);
    }
  }, [API_BASE, DEFAULT_CURRENT_USER]);

  const loadConversation = useCallback(
    async (user1Id = currentUser, user2Id = chatWith) => {
      if (!user1Id || !user2Id) return;

      try {
        const url = `${API_BASE}/api/chat/conversation?user1Id=${user1Id}&user2Id=${user2Id}`;
        const res = await fetch(url);
        const json = await res.json();

        const loadedMessages = json.data ?? json;
        setMessages(Array.isArray(loadedMessages) ? loadedMessages : []);
      } catch (err) {
        console.error("Load conversation lỗi:", err);
      }
    },
    [API_BASE, currentUser, chatWith],
  );

  const connectHub = useCallback(async () => {
    if (!currentUser) return;

    try {
      if (connectionRef.current) {
        await connectionRef.current.stop();
      }

      const connection = new signalR.HubConnectionBuilder()
        .withUrl(`${API_BASE}/chatHub?userId=${currentUser}`)
        .withAutomaticReconnect()
        .build();

      connection.on("ReceiveMessage", (data) => {
        appendMessage(data);
      });

      connection.on("ReceiveOwnMessage", (data) => {
        appendMessage(data);
      });

      connection.on("MessagesRead", () => {});

      connection.onclose(() => {
        setIsConnected(false);
      });

      connection.onreconnecting(() => {
        setIsConnected(false);
      });

      connection.onreconnected(() => {
        setIsConnected(true);
      });

      await connection.start();

      connectionRef.current = connection;
      setIsConnected(true);

      await loadConversation(currentUser, chatWith);
    } catch (err) {
      setIsConnected(false);
      console.error("Connect lỗi:", err);
    }
  }, [API_BASE, currentUser, chatWith, appendMessage, loadConversation]);

  const sendMessage = useCallback(async () => {
    const senderId = Number(currentUser);
    const receiverId = Number(chatWith);
    const content = messageInput.trim();

    if (!senderId || !receiverId || !content) return;

    try {
      const res = await fetch(`${API_BASE}/api/chat/send`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          senderId,
          receiverId,
          content,
          messageType: "TEXT",
        }),
      });

      const json = await res.json();

      if (!res.ok) {
        alert(json.message || "Gửi tin nhắn thất bại");
        return;
      }

      setMessageInput("");
    } catch (err) {
      console.error("Send message lỗi:", err);
    }
  }, [API_BASE, currentUser, chatWith, messageInput]);

  const handleEnterToSend = (e) => {
    if (e.key === "Enter") {
      sendMessage();
    }
  };

  const handleCurrentUserChange = async (e) => {
    const newCurrentUser = Number(e.target.value);
    const newChatUsers = users.filter((u) => u.id !== newCurrentUser);
    const nextChatWith = newChatUsers[0]?.id || 0;

    setCurrentUser(newCurrentUser);
    setChatWith(nextChatWith);
    clearChat();
    setIsConnected(false);

    if (connectionRef.current) {
      await connectionRef.current.stop();
      connectionRef.current = null;
    }
  };

  const handleSelectChatUser = async (userId) => {
    setChatWith(userId);
    clearChat();
    await loadConversation(currentUser, userId);
  };

  useEffect(() => {
    loadUsers();
  }, [loadUsers]);

  useEffect(() => {
    scrollChatToBottom();
  }, [messages, scrollChatToBottom]);

  useEffect(() => {
    if (currentUser && chatWith) {
      connectHub();
    }
  }, [currentUser, chatWith, connectHub]);

  useEffect(() => {
    return () => {
      if (connectionRef.current) {
        connectionRef.current.stop();
      }
    };
  }, []);

  return (
    <div className="chat-page">
      <div className="chat-shell">
        <aside className="chat-sidebar">
          <div className="chat-sidebar-top">
            <div className="chat-sidebar-title">Tin nhắn</div>

            <select
              className="chat-account-select"
              value={currentUser}
              onChange={handleCurrentUserChange}
              disabled={loadingUsers || users.length === 0}
            >
              {users.length === 0 ? (
                <option value={0}>Không có account</option>
              ) : (
                users.map((user) => (
                  <option key={user.id} value={user.id}>
                    {user.fullName || user.username || user.label}
                  </option>
                ))
              )}
            </select>
          </div>

          <div className="chat-user-list">
            {chatUsers.length === 0 ? (
              <div className="chat-empty-sidebar">Không có người để chat</div>
            ) : (
              chatUsers.map((user) => {
                const isActive = Number(chatWith) === Number(user.id);

                return (
                  <button
                    key={user.id}
                    className={`chat-user-item ${isActive ? "active" : ""}`}
                    onClick={() => handleSelectChatUser(user.id)}
                  >
                    <div className="chat-avatar">
                      {(user.fullName || user.username || "U")
                        .charAt(0)
                        .toUpperCase()}
                    </div>

                    <div className="chat-user-meta">
                      <div className="chat-user-name">
                        {user.fullName || user.username || user.label}
                      </div>
                      <div className="chat-user-subtitle">
                        {user.email || user.phoneNumber || "Tài khoản hệ thống"}
                      </div>
                    </div>
                  </button>
                );
              })
            )}
          </div>
        </aside>

        <section className="chat-main">
          <div className="chat-main-header">
            <div className="chat-main-user">
              <div className="chat-avatar large">
                {(chatWithUser?.fullName || chatWithUser?.username || "C")
                  .charAt(0)
                  .toUpperCase()}
              </div>

              <div>
                <div className="chat-main-name">
                  {chatWithUser?.fullName ||
                    chatWithUser?.username ||
                    "Chưa chọn người chat"}
                </div>
                <div className="chat-main-status">
                  {isConnected ? "Đang kết nối" : "Đang ngoại tuyến"}
                </div>
              </div>
            </div>
          </div>

          <div className="chat-main-body" ref={chatBoxRef}>
            {messages.length === 0 ? (
              <div className="chat-empty-state">
                <div className="chat-empty-icon">💬</div>
                <div className="chat-empty-title">Chưa có tin nhắn</div>
                <div className="chat-empty-text">
                  Hãy bắt đầu cuộc trò chuyện với{" "}
                  {chatWithUser?.fullName ||
                    chatWithUser?.username ||
                    "người dùng này"}
                  .
                </div>
              </div>
            ) : (
              messages.map((msg, index) => {
                const isMe = msg.senderId === Number(currentUser);

                return (
                  <div
                    key={`${msg.id ?? "msg"}-${index}-${msg.createdAt ?? ""}`}
                    className={`chat-row ${isMe ? "me" : "other"}`}
                  >
                    <div className={`chat-bubble ${isMe ? "me" : "other"}`}>
                      <div className="chat-bubble-text">{msg.content}</div>
                      <div className="chat-bubble-time">
                        {formatTime(msg.createdAt)}
                      </div>
                    </div>
                  </div>
                );
              })
            )}
          </div>

          <div className="chat-composer">
            <input
              type="text"
              placeholder="Nhập tin nhắn..."
              value={messageInput}
              onChange={(e) => setMessageInput(e.target.value)}
              onKeyDown={handleEnterToSend}
              disabled={!currentUser || !chatWith}
            />
            <button onClick={sendMessage} disabled={!currentUser || !chatWith}>
              Gửi
            </button>
          </div>
        </section>
      </div>
    </div>
  );
}
