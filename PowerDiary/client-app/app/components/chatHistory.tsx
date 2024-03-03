'use client'

import { useEffect, useState } from 'react'

import { ChatEvent, EventGranularity  } from '@/types'

import Loader from './loader';

export default function ChatHistory() {

  const [granularity, setGranularity] = useState<EventGranularity>("Minute")
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [events, setEvents] = useState<ChatEvent[]>([])

  // use fetch to get the chat history
  useEffect(() => {

    const fetchChatHistory = async () => {
      setError(null)
      try {
        const response = await fetch(`/api/chat/${granularity}`)
        const data = await response.json()
        setIsLoading(false)
        setEvents(data)
      } catch (error) {
        setIsLoading(false)
        setError("Error fetching chat history. Please try again and select a valid value.")
      }
    }

    fetchChatHistory()
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

  const renderEvents = () => {
    if (granularity === 'Day') {
      return events.map((event, index) => (
        <div key={index} className="flex flex-row items-center w-full mt-4 border-b ">
          <h2  className="text-lg font-lg mr-5">{formatDate(new Date(event.dateOccurred))}</h2>
          <ul className="flex flex-col w-full mb-3">
            {event.events.map((event, index) => (
              <li key={index} className="text-md">{event}</li>
            ))}
          </ul>
        </div>
      ))
    }
    else {
      const groupedEventsByDay = Object.groupBy(events, ({ dateOccurred }: { dateOccurred: Date }) => dateOccurred.toString().replace(/T.+/, ''))
      return Object.keys(groupedEventsByDay).map((date, index) => (
        <div key={index} className="flex flex-col items-center w-full mt-4 border-b ">
          <h2  className="text-lg font-bold mr-5">{date}</h2>
          {groupedEventsByDay[date].map((event:ChatEvent, index:number) => (
            <div key={index} className="flex flex-row items-center w-full mt-4 border-b ">
            <h2  className="text-lg font-lg mr-5">{formatDate(new Date(event.dateOccurred))}</h2>
            <ul className="flex flex-col w-full mb-3">
              {event.events.map((event, index) => (
                <li key={index} className="text-md">{event}</li>
              ))}
            </ul>
          </div>
          ))}
        </div>
      ))
    }
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
          <option value="Invalid">Invalid value</option>
        </select>
      </div>
      {error && <div className="text-red-500 mt-5">{error}</div>}
      {!error && renderEvents()}
    </div>
  )
}
