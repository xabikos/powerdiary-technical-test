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

  const formatDate = (date: Date) => {
    if (granularity === "Minute") {
      const minute = date.getMinutes()
      return date.getHours() + ":" + (minute < 10 ? ("0" + date.getMinutes()) : date.getMinutes())
    } else if (granularity === "Hour") {
      return date.getHours() + ":00"
    }
    return date.toLocaleDateString()
  }

  if (isLoading) {
    return <Loader />
  }
  return (
    <div className="flex flex-col w-full">
      <div className="flex flex-row w-full">
        <label className="block mb-2 text-md font-medium text-gray-900 dark:text-white mr-4">Select Granularity for chat events</label>
        <select
          value={granularity}
          onChange={(e) => setGranularity(e.target.value as EventGranularity)}
          className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-1/2 p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
          >
          <option value="Minute">Minute</option>
          <option value="Hour">Hour</option>
          <option value="Day">Day</option>
        </select>
      </div>
      {events.map((event, index) => (
        <div key={index} className="flex flex-row items-center w-full mt-4 border-b ">
          <h2 className="text-lg font-lg mr-5">{formatDate(new Date(event.dateOccurred))}</h2>
          <ul className="flex flex-col w-full mb-3">
            {event.events.map((event, index) => (
              <li key={index} className="text-md">{event}</li>
            ))}
          </ul>
        </div>
      ))}
    </div>
  )
}
