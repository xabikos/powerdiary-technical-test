'use client'

import { useEffect, useState } from 'react'

import { ChatEvent, EventGranularity  } from '@/types'

import Loader from './loader';

export default function ChatHistory() {

  const [granularity, setGranularity] = useState<EventGranularity>("Minute")
  const [isLoading, setIsLoading] = useState(true)
  const [events, setEvents] = useState<ChatEvent[]>([])

  // use fetch to get the chat history
  useEffect(() => {
    try {
      const fetchChatHistory = async () => {
        const response = await fetch(`/api/chat/${granularity}`)
        const data = await response.json()
        setIsLoading(false)
        setEvents(data)
      }

      fetchChatHistory()
    } catch (error) {
      setIsLoading(false)
    }
  }, [granularity])

  if (isLoading) {
    return <Loader />
  }
  return (
    <div>
        Length of events: {events.length}
    </div>
  )
}
