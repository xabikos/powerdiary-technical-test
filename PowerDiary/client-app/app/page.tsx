import ChatHistory from '@/app/components/chatHistory';

export default function Home() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-20">
      <div>
        <h1 className="text-4xl font-bold mb-6">Welcome to Power Diary History Chat App</h1>
        <ChatHistory />
      </div>
      <p className="text-lg mt-8">
        This is an application to access chat history. It is built with Next.js and
        Tailwind CSS.
      </p>
    </main>
  );
}
