export type EventGranularity = "Minute" | "Hour" | "Day"

export type ChatEvent = {
  dateOccurred: Date
  events: string[]
}
