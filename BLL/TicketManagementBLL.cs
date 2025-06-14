using System;

/// <summary>
/// Class for managing ticket responses and statuses in the system.
/// </summary>
public class TicketManagementBLL
{
    /// <summary>
    /// Instance of the ticket repository for database operations.
    /// </summary>
	public TicketManagementBLL()
	{
    #region TechnicianTicketManagementBLL

    /// <summary>
    /// Constructor for TicketManagementBLL that initializes the ticket repository.
    /// </summary>
    /// <param name="ticketId">Id of the ticket to response</param>
    /// <param name="response">Response for the ticket</param>
    /// <returns> Returns true if ticket have response and false in the other case</returns>
    public bool RespondToTicket(int ticketId, string response)
    {
        return _ticketRepository.UpdateTicketResponse(ticketId, response);
    }

    /// <summary>
    /// Closes a ticket by updating its status to "Closed".
    /// </summary>
    /// <param name="ticketId"></param>
    /// <returns>Returns true if ticket can stay close and false in the other case</returns>
    public bool CloseTicket(int ticketId)
    {
        return _ticketRepository.UpdateTicketStatus(ticketId, "Answered", "Closed");
    }

    /// <summary>
    /// Retrieves all tickets from the database.
    /// </summary>
    /// <returns>A list with all the tickets</returns>
    public List<Ticket> GetAllTickets()
    {
        return _ticketRepository.GetAllTickets();
    }

    #endregion
}
}
