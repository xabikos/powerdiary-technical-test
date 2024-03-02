import ChatHistory from '@/app/components/chatHistory';

export default function Home() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-20">
      <h1 className="text-4xl font-bold">Welcome to Power Diary Chat App</h1>
      <ChatHistory />
      <p className="text-lg">
        This is an application to access chat history. It is built with Next.js and
        Tailwind CSS.
      </p>
    </main>
  );
}
